using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TutorNinja.Data;

namespace TutorNinja.Migrations
{
    [DbContext(typeof(AdContext))]
    [Migration("20170924212542_database")]
    partial class database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TutorNinja.Models.Ad", b =>
                {
                    b.Property<int>("AdID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Title");

                    b.HasKey("AdID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Ad");
                });

            modelBuilder.Entity("TutorNinja.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("TutorNinja.Models.Ad", b =>
                {
                    b.HasOne("TutorNinja.Models.Category", "Category")
                        .WithMany("Ads")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
