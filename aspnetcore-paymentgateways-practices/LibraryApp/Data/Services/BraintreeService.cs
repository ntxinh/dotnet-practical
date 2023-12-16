using Braintree;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Data.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration _config;
        public BraintreeService(IConfiguration config)
        {
            _config = config;
        }

        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey = _config.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey = _config.GetValue<string>("BraintreeGateway:PrivateKey"),
            };
            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return CreateGateway();
        }
    }
}