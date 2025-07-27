using System;
using System.Collections.Generic;
using BankingWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingWebAPI.DAL;

public partial class BankingDbContext : DbContext
{
    public BankingDbContext()
    {
    }

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
        TransactionDetails = Set<TransactionDetail>();
        Users = Set<User>();
        UserAccountDetails = Set<UserAccountDetail>();
        UserLoginDetail = Set<UserLoginDetails>();

    }

    public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
    public virtual DbSet<UserLoginDetails> UserLoginDetail { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccountDetail> UserAccountDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<TransactionDetail>().ToTable("TransactionDetails");
        modelBuilder.Entity<UserAccountDetail>().ToTable("UserAccountDetails");
        modelBuilder.Entity<UserLoginDetails>().ToTable("UserLoginDetails");
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
