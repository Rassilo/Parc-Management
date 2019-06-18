namespace ParcProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Sale
    {
        [Key]
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateSale { get; set; }
        public float Value { get; set; }
        public float Cost { get; set; }
    }
}
