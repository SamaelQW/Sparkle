using Sparkle.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace Sparkle.Models
{

    /// <summary>
    /// Model which represents registering <see cref="User"/> 
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// The User UserName
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }


        /// <summary>
        /// The property to <see cref="User"/> email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// The property to <see cref="User"/> age
        /// </summary>
        [Required]
        [Display(Name = "Date of birth")]
        public string Year { get; set; }


        /// <summary>
        /// The property to <see cref="User"/> password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        /// <summary>
        /// Confirm <see cref="User"/> password
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
}
