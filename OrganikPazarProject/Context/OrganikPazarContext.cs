using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Entities;

namespace OrganikPazar.Context;

public partial class OrganikPazarContext : DbContext
{
    public OrganikPazarContext()
    {
    }

    public OrganikPazarContext(DbContextOptions<OrganikPazarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Name=DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(500)
                .HasColumnName("imageurl");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "customer_email_key").IsUnique();

            entity.HasIndex(e => e.City, "idx_customer_city");

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Registerdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registerdate");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Logid).HasName("log_pkey");

            entity.ToTable("log");

            entity.Property(e => e.Logid).HasColumnName("logid");
            entity.Property(e => e.Actiondate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actiondate");
            entity.Property(e => e.Actiontype)
                .HasMaxLength(50)
                .HasColumnName("actiontype");
            entity.Property(e => e.Entity)
                .HasMaxLength(50)
                .HasColumnName("entity");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => e.City, "idx_order_city");

            entity.HasIndex(e => e.Customerid, "idx_order_customer");

            entity.HasIndex(e => e.Productid, "idx_order_product");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'Beklemede'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Totalprice)
                .HasPrecision(10, 2)
                .HasColumnName("totalprice");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_customerid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_productid_fkey");
        });

       
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("product_pkey");

            entity.ToTable("product");

            entity.HasIndex(e => e.Categoryid, "idx_product_category");

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(500)
                .HasColumnName("imageurl");
            entity.Property(e => e.Isfeatured)
                .HasDefaultValue(false)
                .HasColumnName("isfeatured");
            entity.Property(e => e.Productname)
                .HasMaxLength(150)
                .HasColumnName("productname");
            entity.Property(e => e.Rating)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("rating");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("product_categoryid_fkey");
        });

        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
