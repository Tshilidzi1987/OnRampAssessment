using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnRampAssessment.Models
{
    public class CustomerModel
    {
        [Required]
        public int? CustomerID { get; set; }
        [Required]        
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]      
        public string Tel { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public LoginModel Login { get; set; }
    }
}