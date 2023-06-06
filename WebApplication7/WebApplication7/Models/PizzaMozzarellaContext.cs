using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Models;

public partial class PizzaMozzarellaContext : DbContext
{
    public PizzaMozzarellaContext()
    {
    }

    public PizzaMozzarellaContext(DbContextOptions<PizzaMozzarellaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionOrder> PositionOrders { get; set; }

    public virtual DbSet<PossibleAddress> PossibleAddresses { get; set; }

    public virtual DbSet<Zayavka> Zayavkas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DTQF99UB;Initial Catalog=Pizza_Mozzarella;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.ToTable("Administrator");

            entity.Property(e => e.AdministratorId).HasColumnName("AdministratorID");
            entity.Property(e => e.Login).HasMaxLength(20);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.ClientId, "IX_FK_Order_Client");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Sum).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Order_Client");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<PositionOrder>(entity =>
        {
            entity.HasKey(e => new { e.PositionId, e.OrderId });

            entity.ToTable("PositionOrder");

            entity.HasIndex(e => e.OrderId, "IX_FK_PositionOrder_Order");

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Order).WithMany(p => p.PositionOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_PositionOrder_Order");

            entity.HasOne(d => d.Position).WithMany(p => p.PositionOrders)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_PositionOrder_Position");
        });

        modelBuilder.Entity<PossibleAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId);

            entity.ToTable("PossibleAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Location).HasMaxLength(100);
        });

        modelBuilder.Entity<Zayavka>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__zayavka__3213E83F5CFABD70");

            entity.ToTable("zayavka");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.Time)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
