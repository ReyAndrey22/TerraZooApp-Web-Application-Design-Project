using Microsoft.AspNetCore.Identity;
using TerraZooApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace TerraZooApp.Services
{
    public class UsersAuthService : IUsersAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersAuthService(
            UserManager<Users> userManager, 
            SignInManager<Users> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<(bool success, string role)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, string.Empty);
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!result.Succeeded)
            {
                return (false, string.Empty);
            }

            var roles = await _userManager.GetRolesAsync(user);
            return (true, roles.FirstOrDefault() ?? "Visitor");
        }

        public async Task<(bool success, string message)> RegisterAsync(Users user, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return (false, "Email already exists");
            }

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Assign role based on the user's role property
            var roleResult = await _userManager.AddToRoleAsync(user, user.Role ?? "Visitor");
            if (!roleResult.Succeeded)
            {
                return (false, "Failed to assign role");
            }

            return (true, "Registration successful");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<(bool success, string message)> UpdateProfileAsync(Users user, IFormFile? profilePicture)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return (false, "User not found");
            }

            // Update user properties
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingUser.Role = user.Role;

            // Handle profile picture
            if (profilePicture != null && profilePicture.Length > 0)
            {
                try
                {
                    // Create the directory if it doesn't exist
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profile_picture");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

                // Delete old profile picture if exists
                if (!string.IsNullOrEmpty(existingUser.ProfilePicture))
                {
                        var oldFilePath = Path.Combine(uploadsFolder, existingUser.ProfilePicture);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                existingUser.ProfilePicture = fileName;
                }
                catch (Exception ex)
                {
                    return (false, $"Error uploading profile picture: {ex.Message}");
                }
            }

            // Update email and username
            var emailResult = await _userManager.SetEmailAsync(existingUser, user.Email);
            if (!emailResult.Succeeded)
            {
                return (false, string.Join(", ", emailResult.Errors.Select(e => e.Description)));
            }

            var usernameResult = await _userManager.SetUserNameAsync(existingUser, user.UserName);
            if (!usernameResult.Succeeded)
            {
                return (false, string.Join(", ", usernameResult.Errors.Select(e => e.Description)));
            }

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return (true, "Profile updated successfully");
        }
    }
} 