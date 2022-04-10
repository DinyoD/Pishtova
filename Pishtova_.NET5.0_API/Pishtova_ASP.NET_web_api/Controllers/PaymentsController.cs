
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;

    public class PaymentsController: ApiController
    {
        private readonly StripeSettings stripeSettings;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public PaymentsController(
			IOptions<StripeSettings> stripeSettings,
			UserManager<User> userManager,
			IUserService userService)
		{
			this.stripeSettings = stripeSettings.Value;
            this.userManager = userManager;
            this.userService = userService;
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
				return BadRequest(new ErrorResult { Message = e.StripeError.Message });
			}
		}


		[Authorize]
		[HttpPost("customer-portal")]
		public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalRequest req)
		{
			var userId = this.userService.GetUserId(User);
			var userFromDb = await this.userManager.FindByIdAsync(userId);
            if (userFromDb == null)
            {
				return BadRequest(new ErrorResult { Message = "Unauthorized or no user" });
			}
			try
			{
				var options = new Stripe.BillingPortal.SessionCreateOptions
				{
					Customer = userFromDb.CustomerId,
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
				return BadRequest(new ErrorResult { Message = e.StripeError.Message });
			}

		}
	}
}
