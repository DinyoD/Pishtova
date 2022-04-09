namespace Pishtova_ASP.NET_web_api.Model.Payment
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCheckoutSessionRequest
    {
        [Required]
        public string PriceId { get; set; }

        //[Required]
        //public string SuccessUrl { get; set; }

        //[Required]
        //public string FailureUrl { get; set; }
    }
}
