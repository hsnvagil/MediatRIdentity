using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRIdentity.DataAccess.Entities;
using MediatRIdentity.Services.Abstract;

namespace MediatRIdentity.Auth {
    public class SignIn {
        public class Query : IRequest<User> {
            [EmailAddress] public string Email { get; set; }
            [DataType(DataType.Password)] public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, User> {
            private readonly IAuthService _authService;

            public Handler(IAuthService authService) {
                _authService = authService;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken) {
                var result = await _authService.SignIn(request);
                return result;
            }
        }
    }
}