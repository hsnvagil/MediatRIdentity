using System.Threading.Tasks;
using MediatRIdentity.Auth;
using MediatRIdentity.DataAccess.Entities;

namespace MediatRIdentity.Services.Abstract {
    public interface IAuthService {
        Task<User> SignUp(SignUp.Command request);
        Task<User> SignIn(SignIn.Query query);
    }
}