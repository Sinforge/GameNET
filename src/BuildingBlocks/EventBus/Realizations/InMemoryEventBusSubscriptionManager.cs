using EventBus.Abstractions;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Realizations
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public bool IsEmpty => _handlers.Count == 0;


        //Need to set : get string callback when event removed
        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }


        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>();
            HelpToAddSubscription(typeof(TH), eventName, false);
            
            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

        }
        public void AddDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            HelpToAddSubscription(typeof(TH), eventName, true);
        }
        private void HelpToAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if(!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }
            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }
            if(isDynamic)
            {
                _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }

        }



        public void Clear()
        {
            _handlers.Clear();
        }



        private SubscriptionInfo FindDynamicSubsciption<TH>(string eventName)
            where TH: IDynamicIntegrationEventHandler
        {
            return FindSubscription(eventName, typeof(TH));
        }
        private SubscriptionInfo FindTypedSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            return FindSubscription(GetEventKey<T>(), typeof(TH));
        }
        private SubscriptionInfo FindSubscription(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }
            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }



        public void DeleteSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var subInfo = FindTypedSubscription<T, TH>();
            HelpToDelete(GetEventKey<T>(), subInfo);

        }
        public void DeleteDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            var subInfo = FindDynamicSubsciption<TH>(eventName);
            HelpToDelete(eventName, subInfo);
        }
        private void HelpToDelete(string eventName, SubscriptionInfo subscriptionInfo)
        {
            if(subscriptionInfo != null)
            {
                _handlers[eventName].Remove(subscriptionInfo);
                if (_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    if(eventName != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventDeleted(eventName);
                }
            }

        }

        private void RaiseOnEventDeleted(string eventName)
        {
            var @event = OnEventRemoved;
            OnEventRemoved?.Invoke(this, eventName);
        }
        public Type GetEventByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(type => type.Name == eventName);
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            return GetHandlersForEvent(GetEventKey<T>());
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            return HasSubscriptionsForEvent(GetEventKey<T>());   
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            return _handlers[eventName].Any();
        }


    }
}
