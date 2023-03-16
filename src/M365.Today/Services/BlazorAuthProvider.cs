﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Graph;

namespace M365.Today.Services
{
    public class BlazorAuthProvider : IAuthenticationProvider
    {
        private readonly IAccessTokenProviderAccessor accessor;

        public BlazorAuthProvider(IAccessTokenProviderAccessor accessor)
        {
            this.accessor = accessor;
        }

        // Function called every time the GraphServiceClient makes a call
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            // Request the token from the accessor
            var result = await accessor.TokenProvider.RequestAccessToken();

            if (result.TryGetToken(out var token))
            {
                // Add the token to the Authorization header
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Value);
            }
        }
    }
}