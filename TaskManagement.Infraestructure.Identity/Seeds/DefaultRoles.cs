using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Application;

namespace TaskManagement.Infraestructure.Identity.Seeds;

/// <summary>
/// Class responsible for creating default roles in the application.
/// </summary>
public class DefaultRoles
{
    /// <summary>
    /// Creates default roles asynchronously.
    /// </summary>
    /// <param name="roleManager">The role manager instance.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
    }
}