using Inventarization.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReactASPCore.Models;

namespace ReactASPCore
{

    public class ApplicationContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Inventory> InventoryLots { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InventorySetup> Setups { get; set; }
        public DbSet<Department> Departments { get; set; }

        /*public static async Task<SqlConnection> GetConnecton(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand cmd = sqlConnection.CreateCommand();
            SqlDataReader rdr = cmd.ExecuteReader();
            return sqlConnection;
        }*/
        public ApplicationContext()
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Connection is opened");
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=inventarizationDB;Trusted_Connection=True;");
        }


        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
         //    base.OnModelCreating(modelBuilder);
         //    modelBuilder.Entity<Post>(); // для добавления в БД данных колонки
         //    modelBuilder.Entity<Post>().HasData(new Post[]
         //    {
         //        new Post { Title = "Jenny", Description = "Jenny1", AuthorId = 33},
         //        new Post { Title = "Kenny", Description = "Jenny2", AuthorId = 44},
         //        new Post { Title = "Tony", Description = "Jenny3", AuthorId = 55}
         //    });
         //    modelBuilder.Entity<Criminal>().Ignore(c => c.ammount); //для игнорирования свойства
         //    modelBuilder.Entity<User>().Property(b => b.Name).IsRequired();
         //    modelBuilder.Entity<Comment>().HasData(
         //        new Comment { Title = "Common comment", AuthorId = 33});
         //    modelBuilder.Entity<Comment>();
         }*/
    }

}
