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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-12VUSI0\\SQLEXPRESS;initial catalog=QuestionWarehouse;user id=sa;password=12345;Integrated Security=True;TrustServerCertificate=True");
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
            entity.Property(e => e.UserAnswer).HasColumnName("userAnswer");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
