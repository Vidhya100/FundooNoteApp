using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
           : base(options)
        {
        }
        //Dbset create a teable
        public DbSet<UserEntity> Usertable { get; set; }
        public DbSet<NotesEntity> NotesTable { get; set; }
        public DbSet<CollabratorEntity> CollabTable { get; set; }
    }
}

