using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Parantez içerisinde database server adresi yazılır.
            //Ancak bizim kendi bilgisayarımızda sql server'a ulaşacağımız için Server=(localdb)\MSSQLLocalDB; yazarız.
            //Database=Northwind; ile database adını belirtiriz.
            //Trusted_Connection=true ile sql server'a kullanıcı adı, şifre gerektirmeden bağlanmayı etkin kılar.
            //Trusted_Connection=true kısmında kullanıcı adı, şifre de kullanılabilir.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        //DbSet<> ile class'ı veritabanındaki tabloya bağlama işlemi aşağıdaki şekilde yapıldı.
        public DbSet<Product> Products { get; set; }        //Product nesnesini Products tablosuna bağla
        public DbSet<Category> Categories { get; set; }     //Category nesnesini Categories tablosuna bağla
        public DbSet<Customer> Customers { get; set; }      //Customer nesnesini Customers tablosuna bağla
        public DbSet<Order> Orders { get; set; }            //Order nesnesini Orders tablosuna bağla
    }
}
