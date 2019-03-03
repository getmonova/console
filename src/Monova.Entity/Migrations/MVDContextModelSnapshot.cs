﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monova.Entity;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Monova.Entity.Migrations
{
    [DbContext(typeof(MVDContext))]
    partial class MVDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Monova.Entity.MVDMonitor", b =>
                {
                    b.Property<Guid>("MonitorId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastCheckDate");

                    b.Property<int>("LoadTime");

                    b.Property<short>("MonitorStatus");

                    b.Property<int>("MonitorTime");

                    b.Property<string>("Name");

                    b.Property<short>("TestStatus");

                    b.Property<decimal>("UpTime");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<Guid>("UserId");

                    b.HasKey("MonitorId");

                    b.ToTable("Monitor");
                });

            modelBuilder.Entity("Monova.Entity.MVDMonitorStep", b =>
                {
                    b.Property<Guid>("MonitorStepId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Interval");

                    b.Property<DateTime>("LastCheckDate");

                    b.Property<Guid>("MonitorId");

                    b.Property<string>("Settings");

                    b.Property<short>("Status");

                    b.Property<short>("Type");

                    b.HasKey("MonitorStepId");

                    b.ToTable("MonitorStep");
                });

            modelBuilder.Entity("Monova.Entity.MVDMonitorStepLog", b =>
                {
                    b.Property<Guid>("MonitorStepLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("Interval");

                    b.Property<string>("Log");

                    b.Property<Guid>("MonitorId");

                    b.Property<Guid>("MonitorStepId");

                    b.Property<DateTime>("StartDate");

                    b.Property<short>("Status");

                    b.HasKey("MonitorStepLogId");

                    b.ToTable("MonitorStepLog");
                });

            modelBuilder.Entity("Monova.Entity.MVDPayment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Currency");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Provider");

                    b.Property<Guid>("SubscriptionId");

                    b.Property<string>("Token");

                    b.Property<Guid>("UserId");

                    b.HasKey("PaymentId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Monova.Entity.MVDSubscription", b =>
                {
                    b.Property<Guid>("SubscriptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<short>("PaymentPeriod");

                    b.Property<DateTime>("StartDate");

                    b.Property<Guid>("SubscriptionTypeId");

                    b.Property<Guid>("UserId");

                    b.HasKey("SubscriptionId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("Monova.Entity.MVDSubscriptionFeature", b =>
                {
                    b.Property<Guid>("SubscriptionFeatureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid>("SubscriptionId");

                    b.Property<Guid>("SubscriptionTypeFeatureId");

                    b.Property<Guid>("SubscriptionTypeId");

                    b.Property<string>("Title");

                    b.Property<string>("Value");

                    b.Property<string>("ValueRemained");

                    b.Property<string>("ValueUsed");

                    b.HasKey("SubscriptionFeatureId");

                    b.ToTable("SubscriptionFeature");
                });

            modelBuilder.Entity("Monova.Entity.MVDSubscriptionType", b =>
                {
                    b.Property<Guid>("SubscriptionTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsPaid");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title");

                    b.HasKey("SubscriptionTypeId");

                    b.ToTable("SubscriptionType");
                });

            modelBuilder.Entity("Monova.Entity.MVDSubscriptionTypeFeature", b =>
                {
                    b.Property<Guid>("SubscriptionTypeFeatureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsFeature");

                    b.Property<string>("Name");

                    b.Property<short>("Sort");

                    b.Property<Guid>("SubscriptionTypeId");

                    b.Property<string>("Title");

                    b.Property<string>("Value");

                    b.HasKey("SubscriptionTypeFeatureId");

                    b.ToTable("SubscriptionTypeFeature");
                });

            modelBuilder.Entity("Monova.Entity.MVDUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Monova.Entity.MVDUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Monova.Entity.MVDUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Monova.Entity.MVDUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Monova.Entity.MVDUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
