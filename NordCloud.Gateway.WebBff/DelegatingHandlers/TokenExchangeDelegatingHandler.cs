using IdentityModel.AspNetCore.AccessTokenManagement;

namespace NordCloud.Gateway.WebBff.DelegatingHandlers
{
    public class TokenExchangeDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;


        public TokenExchangeDelegatingHandler(IHttpClientFactory httpClientFactory,
            IClientAccessTokenCache clientAccessTokenCache)
        {
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClientFactory = httpClientFactory;
        }
    }
}
