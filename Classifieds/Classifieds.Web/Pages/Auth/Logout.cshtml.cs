using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Classifieds.Web.Pages.Auth;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync();
        return LocalRedirect("~/");
    }
}
