﻿using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Application.Common.Interfaces.Repository;
using MediatR;

namespace ChatApp.Application.Message.Get
{
    public class GetMessagesQueryHandler(IChatMessageRepository _chatMessageRepository, IChatMessageRepository chatMessageRepository) : IRequestHandler<GetMessagesQuery, UserMessagesDto>
    {
        public async Task<UserMessagesDto> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatMessageRepository.GetUserMessagesByIdAsync(request.UserId, cancellationToken);
            return messages;
        }
    }
}
