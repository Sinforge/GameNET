using System.Text.Json.Serialization;

namespace EventBus.Events
{
    public record IntegrationEvent
    {
        [JsonInclude]
        public Guid Id { get; private init; }
        [JsonInclude]
        public DateTime CreationDate { get; private init; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
        
        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

    }
}