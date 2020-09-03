using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRIdentity.DataAccess.Entities;
using MediatRIdentity.Services.Abstract;

namespace MediatRIdentity.Auth {
    public class SignUp {
        public class Command : IRequest<User> {
            [EmailAddress] public string Email { get; set; }

            [Required] public string Username { get; set; }

            [Required] public string FirstName { get; set; }

            [Required] public string LastName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }

        public class Handler : IRequestHandler<Command, User> {
            private readonly IAuthService _authService;

            public Handler(IAuthService authService) {
                _authService = authService;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken) {
                var result = await _authService.SignUp(request);
                return result;
            }
        }
    }
}