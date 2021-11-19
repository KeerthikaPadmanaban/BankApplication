using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project1.Models;
using Project1.ViewModel;

#nullable disable

namespace Project1.Models
{
    public partial class onlinebankingContext : DbContext
    {
        public onlinebankingContext()
        {
        }

        public onlinebankingContext(DbContextOptions<onlinebankingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }
        public virtual DbSet<AdminLogin> AdminLogins { get; set; }
        public virtual DbSet<TransferDetail> TransferDetails { get; set; }
    //public virtual DbSet<Statement> state { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data source=STS562L-GOKULKA\\SQLEXPRESS;user id=sa;password=spanindia; Database=onlinebanking;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountDetail>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__Account___CBA1B2576863DE17");

                entity.ToTable("Account_Details");

                entity.Property(e => e.Userid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.AadharNumber).HasColumnName("Aadhar_Number");

                entity.Property(e => e.AccNumber)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Acc_Number");

                entity.Property(e => e.AccType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Acc_Type");

                entity.Property(e => e.AnnualIncome).HasColumnName("Annual_income");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.ConfirmPassword).HasColumnName("confirm_password");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Email_Id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Last_name");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Mobile_Number")
                    .IsFixedLength(true);

                entity.Property(e => e.NomineeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Nominee_Name");

                entity.Property(e => e.Occupation)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PanCardNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PanCard_Number");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.PermtAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Permt_Address");

                entity.Property(e => e.TempAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Temp_Address");
            });

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.HasKey(e => new { e.AccNumber, e.Userid })
                    .HasName("PK__Account___547A92CDD1358E85");

                entity.ToTable("Account_status");

                entity.Property(e => e.AccNumber).HasColumnName("Acc_Number");

                entity.Property(e => e.Userid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.AccStatus).HasColumnName("Acc_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AccountStatuses)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account_s__useri__3B75D760");
            });

            modelBuilder.Entity<AdminLogin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__Admin_Lo__4A3006F7869A9B7B");

                entity.ToTable("Admin_Login");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Admin_Id");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasColumnName("Admin_Password");
            });

            modelBuilder.Entity<TransferDetail>(entity =>
            {
                entity.HasKey(e => e.TransactionNumber)
                    .HasName("PK__Transfer__14112F5740690AB5");

                entity.ToTable("Transfer_Details");

                entity.Property(e => e.TransactionNumber).HasColumnName("Transaction_Number");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.CurrentBalance)
                    .HasColumnType("money")
                    .HasColumnName("current_balance");

                entity.Property(e => e.DepositAmount)
                    .HasColumnType("money")
                    .HasColumnName("Deposit_Amount");

                entity.Property(e => e.FromAccNumber).HasColumnName("From_Acc_Number");

                entity.Property(e => e.ModeOfTransaction)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Mode_Of_Transaction");

                entity.Property(e => e.ToAccNumber).HasColumnName("To_Acc_Number");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Transaction_Date");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.TypeOfTransaction)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Type_Of_Transaction");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.WithdrawAmount)
                    .HasColumnType("money")
                    .HasColumnName("Withdraw_Amount");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransferDetails)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transfer___useri__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Project1.ViewModel.Statement> Statement { get; set; }
    }
}
