using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HOUPE.Models
{
    public class UserContext : IdentityDbContext<User, UserRole, string>
    {
        public DbSet<User> User { get; set; }
        public DbSet<UsersImage> UsersImages { get; set; }
        public DbSet<UsersRoom> UsersRooms { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ImageHotel> ImageHotels { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TravelBox> TravelBoxs { get; set; }


        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
          //  modelBuilder.Ignore<User>();
           // modelBuilder.Ignore<UsersImage>();

            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UsersImageConfiguration());
            modelBuilder.ApplyConfiguration(new UsersRoomConfiguration());
            modelBuilder.ApplyConfiguration(new TourConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new ImageHotelConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new TravelBoxsConfiguration());

        }

        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Password).IsRequired().HasMaxLength(20);
                builder.Property(u => u.FirstName).IsRequired().HasMaxLength(20);
                builder.Property(u => u.LastName).IsRequired().HasMaxLength(20);
            }
        }

        public class UsersImageConfiguration : IEntityTypeConfiguration<UsersImage>
        {
            public void Configure(EntityTypeBuilder<UsersImage> builder)
            {
                builder.HasKey(ui => ui.Id);
                builder.HasOne(ui => ui.Users)
                       .WithOne(ui => ui.UsersImages)
                       .HasForeignKey<UsersImage>(ui => ui.UserId);
            }
        }

        public class UsersRoomConfiguration : IEntityTypeConfiguration<UsersRoom>
        {
            public void Configure(EntityTypeBuilder<UsersRoom> builder)
            {
                builder.HasKey(ur => ur.Id);
                builder.HasOne(ur => ur.Users)
                       .WithOne(t => t.UsersRooms)
                       .HasForeignKey<UsersRoom>(b => b.UserId);
            }
        }

        public class TourConfiguration : IEntityTypeConfiguration<Tour>
        {
            public void Configure(EntityTypeBuilder<Tour> builder)
            {
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Tourss).IsRequired().HasMaxLength(20);
            }
        }

        public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
        {
            public void Configure(EntityTypeBuilder<Hotel> builder)
            {
                builder.HasKey(ur => ur.Id);
                builder.Property(u => u.QuantityNight).IsRequired().HasMaxLength(2);
                builder.HasOne(ur => ur.Tours)
                       .WithMany(t => t.Hotels)
                       .HasForeignKey(b => b.ToursId);
            }
        }

        public class ImageHotelConfiguration : IEntityTypeConfiguration<ImageHotel>
        {
            public void Configure(EntityTypeBuilder<ImageHotel> builder)
            {
                builder.HasKey(ur => ur.Id);
                builder.HasOne(ur => ur.Hotels)
                       .WithMany(ur => ur.ImageHotels)
                       .HasForeignKey(ur => ur.HotelId);
            }
        }

        public class OrderConfiguration : IEntityTypeConfiguration<Order>
        {
            public void Configure(EntityTypeBuilder<Order> builder)
            {
                builder.HasKey(ur => ur.Id);
                builder.HasOne(ur => ur.Users)
                       .WithOne(t => t.Orders)
                       .HasForeignKey<Order>(b => b.UsersId);

                builder.HasOne(ur => ur.Hotels)
                       .WithOne(t => t.Orders)
                       .HasForeignKey<Order>(b => b.HotelsId);
            }
        }

        public class TravelBoxsConfiguration : IEntityTypeConfiguration<TravelBox>
        {
            public void Configure(EntityTypeBuilder<TravelBox> builder)
            {
                builder.HasKey(ur => ur.Id);
                builder.HasOne(ur => ur.Users)
                       .WithMany(t => t.TravelBoxs)
                       .HasForeignKey(b => b.UsersId);

                builder.HasOne(ur => ur.Hotels)
                       .WithOne(t => t.TravelBoxs)
                       .HasForeignKey<TravelBox>(b => b.HotelsId);
            }
        }

    }
}
