using Microsoft.AspNetCore.Authorization;

namespace API.Helpers.Authorizations
{
    public class CustomAuthorizations
    {

        public class CompanyMembers : IAuthorizationRequirement
        {
            public string Domain { get; }
            public CompanyMembers(string domain)
            {
                Domain = domain;
            }

        }
    }
}
