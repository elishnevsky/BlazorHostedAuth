using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BlazorHostedAuth.Server.Pages.Account;

[Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
public class LoginModel : PageModel
{
    public async Task<IActionResult> OnGetAsync(string returnUrl = "/")
    {
        var windowsIdentity = HttpContext.User.Identity!;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, windowsIdentity.Name!),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return LocalRedirect(returnUrl);
    }
}
