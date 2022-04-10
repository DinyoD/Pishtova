namespace Pishtova_ASP.NET_web_api.Model.Payment
{
    using System.ComponentModel.DataAnnotations;

    public class CustomerPortalRequest
    {
        [Required]
        public string ReturnUrl { get; set; }
    }
}
