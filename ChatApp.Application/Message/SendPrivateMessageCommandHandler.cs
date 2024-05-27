using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Constants;
using ChatApp.Application.Common.Dtos.SignalR;
using ChatApp.Application.Common.Exceptions;
using ChatApp.Application.Common.Interfaces;
using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;
using MediatR;

namespace ChatApp.Application.Message
{
    public class SendPrivateMessageCommandHandler(IChatMessageRepository _chatMessageRepository, IMessageHubService _messageHubService, IConnectionPool _connectionCache, IUserRepository _userRepository) : IRequestHandler<SendPrivateMessageCommand>
    {
        public async Task Handle(SendPrivateMessageCommand request, CancellationToken cancellationToken)
        {

            var message = new ChatMessage
            {
                FromUserId = request.FromUserId,
                ToUserId = request.ToUserId,
                MessageText = request.MessageText,
                Timestamp = DateTime.UtcNow,
                ReadStatus = false,
                CreatedDate = DateTime.UtcNow
            };

            await _chatMessageRepository.AddAsync(message, cancellationToken);

            var senderUser = await _userRepository.GetByIdAsync(request.FromUserId, cancellationToken);
            if (senderUser == null)
            {
                throw new CustomException("Sender user not found");
            }


            var connectionIds = _connectionCache.GetConnectionIds(message.ToUserId);
            if (!connectionIds.Any())
            {
                //throw new Exception("No active connections for the recipient");
                //notification
            }

            foreach (var connectionId in connectionIds)
            {
                await _messageHubService.SendMessageAsync(new MessageSignal(
                        message.MessageText,
                        senderUser.Username,
                       senderUser.Id,
                       message.ToUserId.ToString()),
                    connectionId,
                    SignalRConstants.ReceiveMessageFunctionName);
            }

        }
    }
}
