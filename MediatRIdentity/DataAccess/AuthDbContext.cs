using MediatRIdentity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediatRIdentity.DataAccess {
    public class AuthDbContext : IdentityDbContext<User, Role, string> {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}