﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;
namespace CatalogService.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using IdentityModel.Client;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;

    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tokenEndpoint;

        public TokenRefreshMiddleware(RequestDelegate next, string clientId, string clientSecret, string tokenEndpoint)
        {
            _next = next;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tokenEndpoint = tokenEndpoint;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the current token is expired or needs refreshing
            if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var existingToken = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

                var client = new HttpClient();
                var tokenResponse = await client.RequestTokenAsync(new TokenRequest
                {
                    Address = _tokenEndpoint,
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                    GrantType = "client_credentials",
                  
                });

                if (!tokenResponse.IsError)
                {

                    // context.Request.Headers["Authorization"] = $"Bearer {tokenResponse.AccessToken}";
                    var newToken = $"Bearer {tokenResponse.AccessToken}";
                    var authenticationTicket = context.AuthenticateAsync().Result;
                    authenticationTicket.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
                    context.SignInAsync(authenticationTicket.Principal, authenticationTicket.Properties);

                    // Continue the request pipeline
                    await _next(context);
                    return;
                }
            }

            // Continue the request pipeline
            await _next(context);
        }
    }

    public static class TokenRefreshMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenRefreshMiddleware(this IApplicationBuilder builder, string clientId, string clientSecret, string tokenEndpoint)
        {
            return builder.UseMiddleware<TokenRefreshMiddleware>(clientId, clientSecret, tokenEndpoint);
        }
    }

}