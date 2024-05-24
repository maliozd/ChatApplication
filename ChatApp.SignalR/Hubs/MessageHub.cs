using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Hubs
{
    public class MessageHub(IEventPublisher _eventPublisher) : Hub
    {
        // signalr client user mapping => keep connection ids on database, ---> uygulama kapanıp açıldığında connectionlar sıfırlanıyor mu? her yeni bağlantının connection idsi farklı mı?
        // cihaz bilgisi ile cihazda bir secret key tut, secret key ile userid dbye koy
        // gelen istekte userid yi jwt den bul
        // clientta response 401 403 vs dönerse otomatik login
        // webde cookiede olabilir? cookie güvenliği sıkıntı!! desktopda dosya içerisinde
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            //var userId = Context.UserIdentifier;
            //if (userId is null)
            if (Context.UserIdentifier is not null)
            {
                var userId = Convert.ToInt32(Context.UserIdentifier);
                UserConnectedEvent userConnectedEvent = new(userId, connectionId);
                await _eventPublisher.PublishAsync(userConnectedEvent);

                //_cache.AddConnection(Convert.ToInt32(userId), connectionId);
                Console.WriteLine($"Connected : {connectionId}, User Id:{userId}");
            }
            //bağlandı bilgisi ile online offline durumunu değiştirebilirim
            //throw user connected vs event

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.UserIdentifier;

            Console.WriteLine($"User Id :{userId} ");


            await base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// mesaj gönderme işini http ile yapabilirim. öyle yapmam daha iyi olabilir mi?
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="fromUserId"></param>
        /// <param name="messageText"></param>
        /// <returns></returns>
        public async Task SendPrivateMessage(string toUserId, string fromUserId, string messageText)
        {
            //IHttpConnectionFeature feature
            var asd = Context.UserIdentifier;
            var feature = Context.Features[typeof(IHttpConnectionFeature)];

            if (fromUserId == null)
            {
                return;
            }

            else
            {
                // Push notification logic for offline users
                //await _pushNotificationService.SendPushNotificationAsync(toUserId, messageText);
            }
        }

    }
}
