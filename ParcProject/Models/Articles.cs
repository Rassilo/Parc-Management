namespace ParcProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Articles
    {
        [Key]
        public int articleId { get; set; }
        public string articleName { get; set; }
        public string articleNo { get; set; }
        public Nullable<int> articleState { get; set; }
        public string articleStateName { get; set; }
        public Nullable<int> brandNo { get; set; }
        public Nullable<int> genericArticleId { get; set; }
        public string packingUnit { get; set; }
        public Nullable<System.DateTime> Date_Dev { get; set; }
        public string articleNo2 { get; set; }
        public string ArticleNAV { get; set; }
    }
}
