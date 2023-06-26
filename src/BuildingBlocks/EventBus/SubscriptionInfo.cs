using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public record SubscriptionInfo
    {
        public Type HandlerType;
        public bool IsDynamic;

        public SubscriptionInfo(Type HandlerType, bool IsDynamic)
        {
            this.HandlerType = HandlerType;
            this.IsDynamic = IsDynamic;

        }
        public static SubscriptionInfo Typed(Type HandlerType)
        {
            return new SubscriptionInfo(HandlerType, false);
        }
        public static SubscriptionInfo Dynamic(Type HandlerType) 
        {
            return new SubscriptionInfo(HandlerType, true);
        }
    }
}
