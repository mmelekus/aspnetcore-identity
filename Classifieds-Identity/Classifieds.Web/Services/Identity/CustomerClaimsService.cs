using System;
using System.Security.Claims;
using Classifieds.Data.Entities;
using Classifieds.Web.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Classifieds.Web.Services.Identity;

public class CustomerClaimsService : UserClaimsPrincipalFactory<User>
{
    public CustomerClaimsService(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        var isMinimumAge = user.DateOfBirth?.AddYears(18) <= DateTime.Now;

        identity.AddClaim(new(UserClaims.IsMinimumAge, isMinimumAge.ToString()));
        identity.AddClaim(new(UserClaims.FullName, $"{user.FirstName} {user.LastName}"));

        foreach (var role in await UserManager.GetRolesAsync(user))
        {
            identity.AddClaim(new(ClaimTypes.Role, role));
        }

        return identity;
    }
}
