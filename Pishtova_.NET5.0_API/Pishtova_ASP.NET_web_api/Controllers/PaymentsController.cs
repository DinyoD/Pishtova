
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
    using Microsoft.Extensions.Options;

    public class PaymentsController: ApiController
    {
        private readonly StripeSettings stripeSettings;

        public PaymentsController(IOptions<StripeSettings> stripeSettings)
		{
			this.stripeSettings = stripeSettings.Value;
		}

		[HttpPost("create-checkout-session")]
		public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
		{
			var options = new SessionCreateOptions
			{
				SuccessUrl = req.SuccessUrl,
				CancelUrl = req.FailureUrl,
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
					PublicKey = this.stripeSettings.PublicKey
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

		[HttpPost("customer-portal")]
		public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalRequest req)
		{
			try
			{
				var options = new Stripe.BillingPortal.SessionCreateOptions
				{
					Customer = "cus_LTwJyJFmSdCgUb",
					ReturnUrl = req.ReturnUrl,
				};
				var service = new Stripe.BillingPortal.SessionService();
				var session = await service.CreateAsync(options);

				return Ok(new
				{
					url = session.Url
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
	}
}
