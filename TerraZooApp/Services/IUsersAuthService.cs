using TerraZooApp.Models;
using Microsoft.AspNetCore.Http;

namespace TerraZooApp.Services
{
    public interface IUsersAuthService
    {
        Task<(bool success, string role)> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<(bool success, string message)> RegisterAsync(Users user, string password);
        Task<(bool success, string message)> UpdateProfileAsync(Users user, IFormFile? profilePicture);
    }
} 