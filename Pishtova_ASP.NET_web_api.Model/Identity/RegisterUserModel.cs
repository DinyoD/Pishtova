﻿namespace Pishtova_ASP.NET_web_api.Model.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserModel
    {
        [Required]
        [RegularExpression("^[A-Za-z_0-9]+$")]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(4, 12)]
        public int Grade { get; set; }

        public int SchoolId { get; set; }

        public string ClientURI { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
