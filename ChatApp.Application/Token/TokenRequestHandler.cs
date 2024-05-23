using ChatApp.Application.Common.Interfaces;
using MediatR;

namespace ChatApp.Application.Token
{
    internal class TokenRequestHandler(ITokenHandler _tokenHandler) : IRequestHandler<TokenRequest, string>
    {
        public Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            var token = _tokenHandler.GenerateToken(request.Id, request.Username, request.Email);
            return token;
        }
    }
}
