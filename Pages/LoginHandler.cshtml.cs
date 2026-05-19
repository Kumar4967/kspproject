using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ksoproject.Pages;

[AllowAnonymous]
[IgnoreAntiforgeryToken]
public class LoginHandlerModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<LoginHandlerModel> _logger;

    public LoginHandlerModel(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ILogger<LoginHandlerModel> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> OnPostLogin(string email, string password, bool rememberMe, string returnUrl = "/")
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return Redirect("/login?error=" + Uri.EscapeDataString("Email and password are required"));
        }

        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} logged in", email);
            return LocalRedirect(returnUrl);
        }

        if (result.IsLockedOut)
        {
            return Redirect("/login?error=" + Uri.EscapeDataString("Account is locked out. Please try again later."));
        }

        return Redirect("/login?error=" + Uri.EscapeDataString("Invalid email or password"));
    }

    public async Task<IActionResult> OnPostRegister(string email, string password, string confirmPassword, string returnUrl = "/")
    {
        if (password != confirmPassword)
        {
            return Redirect("/login?error=" + Uri.EscapeDataString("Passwords do not match") + "&register=true");
        }

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return Redirect("/login?error=" + Uri.EscapeDataString("Email and password are required") + "&register=true");
        }

        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return Redirect("/login?error=" + Uri.EscapeDataString("User with this email already exists") + "&register=true");
        }

        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} registered", email);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Redirect("/login?error=" + Uri.EscapeDataString(errors) + "&register=true");
    }
}
