using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace FoodOrderCoreProject.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Please enter your name.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress]
        [Remote(action: "CheckEmailIsAlreadyRegistered",controller:"Account",ErrorMessage ="Email is Already registered.")]
        public string? Email { get; set; }
        [PasswordPropertyText]
        [Required(ErrorMessage = "Please enter your password.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter your phone Number.")]
        public long Contact { get; set; }
        public string? Role { get; set; }
    }
}
