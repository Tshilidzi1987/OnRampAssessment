using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnRampAssessment.Models
{
    public class OrderModel
    {
        [Required]
        public int? OrderID { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public string OrderName { get; set; }
        [Required]
        public ProductsModel Products { get; set; }
        [Required]
        public CustomerModel Customer { get; set; }
        [Required]
        public ProductStatusModel ProductStatus { get; set; }
        [Required]
        public SupplierModel Supplier { get; set; }
        [Required]
        public PaymentStatusModel PaymentStatus { get; set; }
        [Required]
        public ProductCategoryModel ProductCategory { get; set; }


    }
}