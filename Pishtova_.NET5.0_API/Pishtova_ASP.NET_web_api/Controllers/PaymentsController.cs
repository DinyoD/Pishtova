using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Pishtova_ASP.NET_web_api.Controllers
{
    public class PaymentsController: ApiController
    {
		[HttpGet("products")]
		public IActionResult Products()
		{

			StripeConfiguration.ApiKey = "sk_test_51KlsUjBd9uAKWbJc1M1IAR6barYfnAUHC3FTwm8uVoqObQ2s2SuipPzOIs4O88kx6Bf3hmFOytnYFqedkrOnq2je00EDSSc9de";

			var options = new ProductListOptions
			{
				Limit = 3,
			};
			var service = new ProductService();
			StripeList<Product> products = service.List(
			  options
			);


			return Ok(products);
		}
	}
}
