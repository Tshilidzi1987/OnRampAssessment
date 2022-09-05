using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnRampAssessment.Models
{
    public class LoginModel
    {
        [Required]
        public int? ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]       
        public string Password { get; set; }
        [Required]
        public LoginTypeModel login_type { get; set; }

    }

    public class LoginTypeModel
    { 
        [Required]
        public int? ID { get; set; }
        public string Type { get; set; }

    }
}