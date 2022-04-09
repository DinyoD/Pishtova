
namespace Pishtova_ASP.NET_web_api.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using Stripe;
	using Stripe.Checkout;

	using Pishtova_ASP.NET_web_api.Model.Payment;
	using Pishtova_ASP.NET_web_api.Model.Results;

    public class PaymentsController: ApiController
    {

		public PaymentsController()
		{
			StripeConfiguration.ApiKey = "sk_test_51KlsUjBd9uAKWbJc1M1IAR6barYfnAUHC3FTwm8uVoqObQ2s2SuipPzOIs4O88kx6Bf3hmFOytnYFqedkrOnq2je00EDSSc9de";
		}

		[HttpPost("create-checkout-session")]
		public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
		{
			var options = new SessionCreateOptions
			{
				SuccessUrl = "http://localhost:4200/success",
				CancelUrl = "http://localhost:4200/failure",
				PaymentMethodTypes = new List<string>
				{
					"card",
				},
				Mode = "subscription",
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						Price = req.PriceId,
						Quantity = 1,
					},
				},
			};

			var service = new SessionService();
			try
			{
				var session = await service.CreateAsync(options);
				return Ok(new CreateCheckoutSessionResponse
				{
					SessionId = session.Id,
				});
			}
			catch (StripeException e)
			{
				Console.WriteLine(e.StripeError.Message);
				return BadRequest(new ErrorResponse
				{
					ErrorResult = new ErrorResult
                    {
						Message = e.StripeError.Message,
					}
				});
			}
		}
		//[HttpGet("products")]
		//public IActionResult Products()
		//{

		//	StripeConfiguration.ApiKey = "sk_test_51KlsUjBd9uAKWbJc1M1IAR6barYfnAUHC3FTwm8uVoqObQ2s2SuipPzOIs4O88kx6Bf3hmFOytnYFqedkrOnq2je00EDSSc9de";

		//	var options = new ProductListOptions
		//	{
		//		Limit = 3,
		//	};
		//	var service = new ProductService();
		//	StripeList<Product> products = service.List(
		//	  options
		//	);


		//	return Ok(products);
		//}

	}
}
