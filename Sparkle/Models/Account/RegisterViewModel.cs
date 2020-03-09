using Sparkle.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace Sparkle.Models
{

    /// <summary>
    /// Model which represents registering <see cref="User"/> 
    /// </summary>
    public class RegisterViewModel
    {

        /// <summary>
        /// The user's name
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// The user's surname
        /// </summary>
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }


        /// <summary>
        /// The User's UserName
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
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }


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
