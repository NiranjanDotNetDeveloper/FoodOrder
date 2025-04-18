using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress]
        public string? Email { get; set; }
        [PasswordPropertyText]
        [Required(ErrorMessage = "Please enter your password.")]
        public string? Password { get; set; }
    }
}
