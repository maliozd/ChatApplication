using MediatR;

namespace ChatApp.Application.Common.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : INotification;
    }
    /// <summary>
    /// 
    /// mediator zaten bunu yapıyor ama ben signalr a mediatr kurmak istemediğim için eventlerimi buradan yayınlayacam
    /// 
    /// </summary>
    /// <param name="_medaitor"></param>
    public class EventPublisher(IMediator _medaitor) : IEventPublisher
    {
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : INotification
        {
            await _medaitor.Publish(@event);
        }
    }
}
