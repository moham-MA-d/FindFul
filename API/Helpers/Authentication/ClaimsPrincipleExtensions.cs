﻿using System.Security.Claims;

namespace API.Helpers.Authentication;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        // ClaimTypes.Name : Represent `JwtRegisteredClaimNames.UniqueName`
        // that we've set inside our token => `TokenService`
        return user.FindFirst("UserName")?.Value;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        // ClaimTypes.NameIdentifier : Represent `JwtRegisteredClaimNames.NameId`
        // that we've set inside our token => `TokenService`
        var value = user.FindFirst("Id")?.Value;
        return value != null ? int.Parse(value) : 0;
    }
}
