//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using Microsoft.EntityFrameworkCore.Infrastructure;

//namespace HallManagement.Model.Entities
//{
//    public partial class BangamataHallContext : DbContext
//    {
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Server=SHARIF-PC\\MSSQLSERVER2019;User Id=bangamatahalldev;Password=bangamatahalldev;Database=BangamataHall;Encrypt=false;TrustServerCertificate=True;Trusted_Connection=True;");
//            optionsBuilder
//                .ConfigureWarnings(warnings => warnings
//                    .Ignore(CoreEventId.DetachedLazyLoadingWarning));
//            optionsBuilder.UseLazyLoadingProxies();
//        }

//    }
//}



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HallManagement.Model.Entities
{
    public partial class BangamataHallContext : DbContext
    {
        //public BangamataHallContext(
        //    DbContextOptions<BangamataHallContext> options)
        //    : base(options)
        //{
        //}

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings
                    .Ignore(CoreEventId.DetachedLazyLoadingWarning));

            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}