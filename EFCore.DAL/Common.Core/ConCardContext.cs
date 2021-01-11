using EFCore.DAL.Common.Interface;
using EFCore.Entity;
using EFCore.Entity.DB_Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.DAL.Common.Core
{
    /// <summary>
    /// db上下文类
    /// </summary>
    public class ConCardContext:DbContext, IconCardContext
    {
        public ConCardContext(DbContextOptions<ConCardContext> options) : base(options)
        {


        }

        public DbSet<Blog_Users> Blog_Users { get; set; }

    }
}
