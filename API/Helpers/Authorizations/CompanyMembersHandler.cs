using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace API.Helpers.Authorizations
{
    public class CompanyMembersHandler : AuthorizationHandler<CustomAuthorizations.CompanyMembers>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizations.CompanyMembers requirement)
        {
            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            
            if (userEmailAddress.EndsWith(requirement.Domain))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
