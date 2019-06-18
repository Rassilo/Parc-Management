
namespace ParcProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class getArticleIdsWithState
    {
        [Key]
        [Column(Order = 0)]
        public string carID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int articleID { get; set; }
        public int SortNo { get; set; }
    }
}
