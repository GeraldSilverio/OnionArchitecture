using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Application;

namespace TaskManagement.Infraestructure.Identity.Seeds;

/// <summary>
/// Class responsible for creating a default user in the application.
/// </summary>
public static class DefaultUser
{
    /// <summary>
    /// Creates a default user asynchronously if it does not already exist.
    /// </summary>
    /// <param name="userManager">The user manager instance.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task CreateUser(UserManager<ApplicationUser> userManager)
    {
        try
        {
            ApplicationUser user = new()
            {
                UserName = "User123",
                Email = "User123@gmail.com",
                FirstName = "User",
                LastName = "User12",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            // Check if the user already exists
            if (userManager.Users.All(x => x.Id != user.Id))
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    // Create the user with a default password
                    await userManager.CreateAsync(user, "123UserC#@");

                    // Assign the 'User' role to the new user
                    await userManager.AddToRoleAsync(user, Roles.User.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during user creation
            throw new ApplicationException(ex.Message, ex);
        }
    }
}