using A.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A.BaseLib.Extension
{
    public static class ModelBuilderExtensions
    {
        public static void Send(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
               new Student()
               {
                   Id = 1,
                   Name = "哈哈哈哈哈哈哈",
                   Grade = Grade.FirstGrade,
                   Mailbox = "121432@qq.com",
                   Photo = ""
               }
           );

            modelBuilder.Entity<Users>().HasData(
                new Users()
                {
                    Id = 1,
                    UserName = "Admin",
                    PassWord = "admin123"
                }
                );
        }
    }
}
