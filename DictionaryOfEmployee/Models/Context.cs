
using Microsoft.EntityFrameworkCore;

namespace DictionaryOfEmployee.Models
{
    public class Context : DbContext
    {
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<User> Users { get; set; } // может быть null для поля отчества
        public DbSet<Employee> Employees { get; set; } = null!;



        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            //Database.EnsureDeleted(); // вызываем, если нужно пересоздать базу
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Добавляем уникальность
            modelBuilder.Entity<Organization>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Position>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(u => u.Email).IsUnique();

            /// Засеивание первоначальными данными
            
            modelBuilder.Entity<Organization>().HasData(new Organization { Id = 1, Name = "TestOrg1" }, new Organization { Id = 2, Name = "TestOrg2" });
            modelBuilder.Entity<Position>().HasData(new Position { Id = 1, Name = "Начальник отдела" }, new Position { Id = 2, Name = "Бухгалтер" });
            modelBuilder.Entity<User>().HasData(new User { Id = 1, LastName = "Иванов", FirstName= "Иван", Patronymic ="Иванович"}, new User { Id = 2, LastName = "Данилов", FirstName = "Толя", Patronymic = "Акакевич" });
            modelBuilder.Entity<Department>().HasData(new Department { Id = 1, OrganizationId= 1, Name = "Dep1.1" }, new Department { Id = 2, OrganizationId = 2, Name = "Dep2.1" });
            modelBuilder.Entity<Employee>().HasData(new Employee { Id = 1, DepartmentId = 2, PositionId = 1, UserId = 1, Email = "test@gmail.com", Telephone="+79999999999" }, new Employee { Id = 2, DepartmentId = 1, PositionId = 2, UserId = 2, Email = "test1@gmail.com", Telephone = "+79999999991" });


        }
    }
}
