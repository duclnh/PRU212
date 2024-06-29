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

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());
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
            entity.Property(e => e.PositionX)
                .HasColumnName("positionX");
            entity.Property(e => e.PositionY)
                .HasColumnName("positionY");
            entity.Property(e => e.PositionZ)
                .HasColumnName("positionZ");
            entity.Property(e => e.Sence)
                .HasMaxLength(20)
                .HasColumnName("sence");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("itemId");
            entity.Property(e => e.SlotId).HasColumnName("slotId");
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

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.Property(e => e.PlantId).ValueGeneratedNever().HasColumnName("plantId");
            entity.Property(e => e.PositionX)
                .HasColumnName("positionX");
            entity.Property(e => e.PositionY)
                .HasColumnName("positionY");
            entity.Property(e => e.PositionZ)
                .HasColumnName("positionZ");
            entity.Property(e => e.Datetime).HasColumnName("datetime");
            entity.Property(e => e.CurrentStage).HasColumnName("currentStage");
            entity.Property(e => e.QuantityHarvested).HasColumnName("quantityHarvested");
            entity.Property(e => e.Crop).IsUnicode(false).HasColumnName("crop");
            entity.Property(e => e.GrowTime).HasColumnName("growTime");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Tiles).HasColumnName("tiles");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.HasOne(d => d.User).WithMany(p => p.Plants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plants_Users");
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.Property(e => e.AnimalId).ValueGeneratedNever().HasColumnName("animalId");
            entity.Property(e => e.PositionX)
                .HasColumnName("positionX");
            entity.Property(e => e.PositionY)
                .HasColumnName("positionY");
            entity.Property(e => e.PositionZ)
                .HasColumnName("positionZ");
            entity.Property(e => e.MoveSpeed)
                .HasColumnName("moveSpeed");
            entity.Property(e => e.NameItem).IsUnicode(false).HasColumnName("nameItem");
            entity.Property(e => e.GrowTime).HasColumnName("growTime");
            entity.Property(e => e.NumberStage).HasColumnName("numberStage");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ItemName).IsUnicode(false).HasColumnName("itemName");
            entity.Property(e => e.Datetime).HasColumnName("datetime");
            entity.Property(e => e.CurrentStage).HasColumnName("currentStage");
            entity.Property(e => e.QuantityHarvested).HasColumnName("quantityHarvested");
            entity.Property(e => e.PriceHarvested).HasColumnName("priceHarvested");
            entity.Property(e => e.Hungry).HasColumnName("hungry");
            entity.Property(e => e.Sick).HasColumnName("sick");
            entity.Property(e => e.LocalScaleX).HasColumnName("localScaleX");
            entity.Property(e => e.LocalScaleY).HasColumnName("localScaleY");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.HasOne(d => d.User).WithMany(p => p.Animals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Animals_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
