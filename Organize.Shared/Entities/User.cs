using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Organize.Shared.Entities
{
    public class User
    {
        [Required]
        [StringLength(10, ErrorMessage = "Username is to long!")]
        public string  UserName { get; set; }

        [Required(ErrorMessage = "The password is required!!!")]
        public string Password { get; set; }
    }
}
