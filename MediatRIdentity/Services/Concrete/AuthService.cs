using System.Threading.Tasks;
using MediatRIdentity.Auth;
using MediatRIdentity.DataAccess.Entities;
using MediatRIdentity.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace MediatRIdentity.Services.Concrete {
    public class AuthService : IAuthService {
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
                           RoleManager<Role> roleManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<User> SignUp(SignUp.Command request) {
            var existUser = await _userManager.FindByEmailAsync(request.Email);
            if (existUser != null) return null;

            var newUser = new User {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (result.Succeeded == false) return null;

            var roleExistAsync = await _roleManager.RoleExistsAsync("User");
            if (roleExistAsync == false)
                await _roleManager.CreateAsync(new Role {
                    Name = "User"
                });

            result = await _userManager.AddToRoleAsync(newUser, "User");
            return result.Succeeded ? newUser : null;
        }

        public async Task<User> SignIn(SignIn.Query query) {
            var existUser = await _userManager.FindByEmailAsync(query.Email);
            if (existUser == null) return null;


            var result = await _signInManager.PasswordSignInAsync(existUser, query.Password, false, false);
            return result.Succeeded ? existUser : null;
        }
    }
}