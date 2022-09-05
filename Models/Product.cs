using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnRampAssessment.Models
{
    public class ProductsModel
    {
        [Required]
        public string Barcode { set; get; }
        [Required]
        public string ProductName { set; get; }
        [Required]
        public string DateCaptured { set; get; }       
        public string Location { set; get; }
        public bool? Warranty { set; get; }
        public ProductCategoryModel ProductCategory { set; get; }
        public ProductStatusModel ProductStatus { set; get; }  
        public SupplierModel Supplier { set; get; }    
        
    }
    public class ProductCategoryModel
    {
        [Required]
        public int ProductCategoryID { set; get; }
        [Required]
        public string CategoryName { set; get; }
        public string CategoryNameAndDesc { set; get; }
        public string CategoryDescription { set; get; }
    }
    public class ProductStatusModel
    {
        [Required]
        public int? ProductStatusID { set; get; }
        public string StatusDescription { set; get; }      
    }

    public class SupplierModel
    {
        [Required]
        public int? SupplierID { set; get; }
        [Required]
        public string Name { set; get; }
        public string Email { set; get; }
        public string Tel { set; get; }
        public string Address { set; get; }
    }

    public class ProductPie
    { 
        public string ProductCategory { get; set; }    
        public int Count { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}