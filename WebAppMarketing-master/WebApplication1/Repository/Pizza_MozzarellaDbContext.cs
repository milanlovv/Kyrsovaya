using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repository.Models;

namespace WebApplication1.Repository;

public partial class Pizza_MozzarellaDbContext : DbContext
{
    public Pizza_MozzarellaDbContext()
    {
    }

    public Pizza_MozzarellaDbContext(DbContextOptions<Pizza_MozzarellaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Administrator1> Administrators1 { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Client1> Clients1 { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Employee1> Employees1 { get; set; }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<FavouriteProduct> FavouriteProducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Order1> Orders1 { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionOrder> PositionOrders { get; set; }

    public virtual DbSet<PossibleAddress> PossibleAddresses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DTQF99UB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasOne(d => d.Client).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carts__ClientID__49C3F6B7");
        });

        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasOne(d => d.Cart).WithMany(p => p.CartProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart_Prod__CartI__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.CartProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart_Prod__Produ__4CA06362");
        });

        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.HasOne(d => d.Client).WithMany(p => p.Favourites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favourite__Clien__5070F446");
        });

        modelBuilder.Entity<FavouriteProduct>(entity =>
        {
            entity.HasOne(d => d.Favourite).WithMany(p => p.FavouriteProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favourite__Favou__5441852A");

            entity.HasOne(d => d.Product).WithMany(p => p.FavouriteProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favourite__Produ__534D60F1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasConstraintName("FK_Order_Client");
        });

        modelBuilder.Entity<Order1>(entity =>
        {
            entity.HasOne(d => d.Client).WithMany(p => p.Order1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ClientID__267ABA7A");
        });

        modelBuilder.Entity<PositionOrder>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.PositionOrders).HasConstraintName("FK_PositionOrder_Order");

            entity.HasOne(d => d.Position).WithMany(p => p.PositionOrders).HasConstraintName("FK_PositionOrder_Position");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.ProductOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_O__Order__2C3393D0");

            entity.HasOne(d => d.Product).WithOne(p => p.ProductOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_O__Produ__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
