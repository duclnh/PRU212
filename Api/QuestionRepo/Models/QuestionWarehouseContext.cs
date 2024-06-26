using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuestionRepo.Models;

public partial class QuestionWarehouseContext : DbContext
{
    public QuestionWarehouseContext()
    {
    }

    public QuestionWarehouseContext(string connectionString)
    {
        this.Database.SetConnectionString(connectionString);
    }

    public QuestionWarehouseContext(DbContextOptions<QuestionWarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("data source=DESKTOP-V5JCDV7\\LOCALHOST;initial catalog=QuestionWarehouse;user id=sa;password=12345;Integrated Security=True;TrustServerCertificate=True");
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.QuestionId)
                .ValueGeneratedNever()
                .HasColumnName("questionId");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.Question1).HasColumnName("question");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.Property(e => e.RecordId)
                .ValueGeneratedNever()
                .HasColumnName("recordId");
            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Question).WithMany(p => p.Records)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Records_Questions");

            entity.HasOne(d => d.User).WithMany(p => p.Records)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Records_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Money)
                .HasColumnName("money");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("itemId");
            entity.Property(e => e.ItemName).IsUnicode(false).HasColumnName("itemName");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Type).IsUnicode(false).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Items)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
