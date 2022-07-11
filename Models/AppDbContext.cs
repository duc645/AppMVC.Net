using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.DbContext;
namespace cs68.models{
   public class AppDbContext : DbContext
   {
      
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
       {

       }

       protected override void OnConfiguring(DbContextOptionsBuilder builder){
           base.OnConfiguring(builder);
       }
       
       protected override void OnModelCreating(ModelBuilder modelBuilder){
           base.OnModelCreating(modelBuilder);
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //     {
        //         var tableName = entityType.GetTableName();
        //         if(tableName.StartsWith("AspNet")){
        //             entityType.SetTableName(tableName.Substring(6));
        //         }
                
        //     }
       }


        //public DbSet<Article> articles{get;set;} 
       //Dbset la mot tap hop chua cac phan tu kieu Article
       //khai bao nhu nay thi trong csdl se co bang articles, co cac dong theo kieu du lieu Article



    //    migration : o trang thai "pending" : chua duoc su dung de tao ra csdl , chua dc cap nhat len sql server
   }
}