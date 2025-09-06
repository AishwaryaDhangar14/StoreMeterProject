namespace Payroll.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using TrackerEnabledDbContext;
    using global::Payroll.Models;

    public partial class Soft_Entity : TrackerContext
    {
        public Soft_Entity()
            : base("name=Soft_Entity")
        {

        }

        public Soft_Entity(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public Soft_Entity get_Soft_Entity(string brcode)
        {
            IDBConnect con = new DBConnect();
            string conStr = con.getConnectionStr(brcode);
            var db = new Soft_Entity(conStr);
            return db;
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Stores> Stores { get; set; }

    }
}
