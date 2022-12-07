
namespace Pishtova_ASP.NET_web_api.Controllers
{
	using System;
    using System.IO;
    using System.Linq;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

	using Stripe;
	using Stripe.Checkout;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
	using Pishtova_ASP.NET_web_api.Model.Payment;
	using Pishtova_ASP.NET_web_api.Model.Results;
	using Microsoft.EntityFrameworkCore;

	public class PaymentsController: ApiController
    {
        private readonly StripeSettings stripeSettings;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IPishtovaSubscriptionService subscriptionService;

        public PaymentsController(
			IOptions<StripeSettings> stripeSettings,
			UserManager<User> userManager,
			IUserService userService,
			IPishtovaSubscriptionService subscriptionService)
		{
			this.stripeSettings = stripeSettings.Value;
            this.userManager = userManager;
            this.userService = userService;
            this.subscriptionService = subscriptionService;
        }

		[HttpGet("product")]
		public IActionResult GetProduct()
        {
            try
            {
				var priceService = new PriceService();
				var productService = new ProductService();
				var prices = priceService
					.List()
					.Select(x => new StripePriceModel
					{
						Id = x.Id,
						Subscription = x.Recurring?.Interval,
						Description = x.Nickname,
						Price = (double)(x.UnitAmount != null ? x.UnitAmount / 100.00 : 0.00),
                    })
                    .ToList();

				var product = productService
					.List()
					.Select(x => new StripeProductModel
					{
						Id = x.Id,
						Name = x.Name,
						Description =x.Description
					})
					.FirstOrDefault();
				product.Prices = prices;
                return Ok(product);
            }
			catch (StripeException e)
			{
				return BadRequest(new ErrorResult { Message = e.StripeError.Message });
			}
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
			var userId = await this.userService.GetUserIdAsync(User);
			var userFromDb = await this.userManager.Users.FirstOrDefaultAsync(x =>x.Id == userId);
            if (userFromDb == null) return BadRequest(new ErrorResult { Message = "Unauthorized or no user" });

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

		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(
				 json,
				 Request.Headers["Stripe-Signature"],
				 this.stripeSettings.WHSecret
			   );

				// Handle the event
				if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
				{
					var subscription = stripeEvent.Data.Object as Subscription;
					await AddSubscriptionToDbAsync(subscription);
				}
				else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
				{
					var session = stripeEvent.Data.Object as Subscription;
					await UpdateSubscriptionAsync(session);
				}
				else if (stripeEvent.Type == Events.CustomerCreated)
				{
					var customer = stripeEvent.Data.Object as Customer;
					await AddCustomerIdToUserAsync(customer);
				}
				// ... handle other event types
				else
				{
					// Unexpected event type
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}
				return Ok();
			}
			catch (StripeException e)
			{
				Console.WriteLine(e.StripeError.Message);
				return BadRequest(new ErrorResult { Message = e.StripeError.Message });
			}
		}

		private async Task AddCustomerIdToUserAsync(Customer customer)
		{
			try
			{
				var userFromDb = await this.userManager.FindByEmailAsync(customer.Email);

				if (userFromDb != null)
				{
					userFromDb.CustomerId = customer.Id;
					await this.userManager.UpdateAsync(userFromDb);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to add customer id to user");
				Console.WriteLine(ex);
			}
		}

		private async Task AddSubscriptionToDbAsync(Subscription subscription)
		{
			try
			{
				var subscriptoin = new Subsription
				{
					Id = subscription.Id,
					CustomerId = subscription.CustomerId,
					Status = "active",
					CurrentPeriodEnd = subscription.CurrentPeriodEnd
				};
				await this.subscriptionService.CreateAsync(subscriptoin);

				//TODO Can send an email welcoming the new subscriber
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("Unable to add new subscriber to Database");
				Console.WriteLine(ex.Message);
			}
		}

		private async Task UpdateSubscriptionAsync(Subscription subscription)
		{
			try
			{
				var subscriptionFromDb = await this.subscriptionService.GetByIdAsync(subscription.Id);
				if (subscriptionFromDb != null)
				{
					subscriptionFromDb.Status = subscription.Status;
					subscriptionFromDb.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
					await this.subscriptionService.UpdateAsync(subscriptionFromDb);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("Unable to update subscription");
				Console.WriteLine(ex.Message);
			}
		}
	}
}
