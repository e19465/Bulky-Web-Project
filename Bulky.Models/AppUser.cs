using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bulky.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "Please enter your name")]
        public required string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
    }
}
