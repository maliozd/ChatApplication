using ChatApp.Application.Common.Interfaces.Events;
using MediatR;

namespace ChatApp.Application.Common.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IWebSocketConnectionEvent;
    }
    public class EventPublisher(IMediator _medaitor) : IEventPublisher
    {
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IWebSocketConnectionEvent
        {
            await _medaitor.Publish(@event);
        }
    }
}
