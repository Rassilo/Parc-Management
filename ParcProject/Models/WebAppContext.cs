using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParcProject.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class WebAppContext : DbContext
    {
       
        public WebAppContext() : base("WebAppCon") { }
        public virtual DbSet<Articles> Article { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<ArticleOrigin> ArticleOrigins { get; set; }
        public virtual DbSet<ArticleAttributes> ArticleAttributes { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<getArticleIdsWithState> getArticleIdsWithState { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Manufacter> Manufacter { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<OriginesOEN> OriginesOEN { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<PARCAR> PARCAR { get; set; }
    }
}