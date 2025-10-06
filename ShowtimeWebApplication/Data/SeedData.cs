using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowtimeWebApplication.Models;

namespace ShowtimeWebApplication.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string[] roleNames = { "Admin", "User" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var adminUser = new ApplicationUser
                {
                    UserName = "admin@showtime.com",
                    Email = "admin@showtime.com",
                    FullName = "System Administrator",
                    EmailConfirmed = true
                };

                string adminPassword = "Admin123!";
                var admin = await userManager.FindByEmailAsync(adminUser.Email);
                if (admin == null)
                {
                    var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createAdmin.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

                var normalUser = new ApplicationUser
                {
                    UserName = "user@showtime.com",
                    Email = "user@showtime.com",
                    FullName = "Customer",
                    EmailConfirmed = true
                };

                string userPassword = "User123!";
                var user = await userManager.FindByEmailAsync(normalUser.Email);
                if (user == null)
                {
                    var createUser = await userManager.CreateAsync(normalUser, userPassword);
                    if (createUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(normalUser, "User");
                    }
                }

                if (!context.Movies.Any())
                {
                    var movies = new[]
                    {
                        new Movie { Title = "The Matrix", Genre = Genre.ScienceFiction, Duration = 136 },
                        new Movie { Title = "Inception", Genre = Genre.ScienceFiction, Duration = 148 },
                        new Movie { Title = "The Dark Knight", Genre = Genre.Action, Duration = 152 },
                        new Movie { Title = "Pulp Fiction", Genre = Genre.Thriller, Duration = 154 },
                        new Movie { Title = "Forrest Gump", Genre = Genre.Drama, Duration = 142 },
                        new Movie { Title = "The Shawshank Redemption", Genre = Genre.Drama, Duration = 142 }
                    };

                    context.Movies.AddRange(movies);
                    await context.SaveChangesAsync();

                    var showtimes = new List<Showtime>();

                    for (int i = 0; i < movies.Length; i++)
                    {
                        showtimes.Add(new Showtime
                        {
                            MovieId = movies[i].Id,
                            StartTime = new DateTime(2025, 9, 15 + i, 18, 0, 0),
                            AvailableSeats = 100,
                            Price = 12.50m + i
                        });

                        showtimes.Add(new Showtime
                        {
                            MovieId = movies[i].Id,
                            StartTime = new DateTime(2025, 9, 15 + i, 21, 0, 0),
                            AvailableSeats = 100,
                            Price = 12.50m + i
                        });
                    }

                    context.Showtimes.AddRange(showtimes);
                    await context.SaveChangesAsync();

                    var adminBookings = new[]
                    {
                        new Booking
                        {
                            UserId = adminUser.Id,
                            ShowtimeId = showtimes[0].Id,
                            NumberOfTickets = 2,
                            BookingDate = DateTime.Now.AddDays(-1)
                        },
                        new Booking
                        {
                            UserId = adminUser.Id,
                            ShowtimeId = showtimes[2].Id,
                            NumberOfTickets = 1,
                            BookingDate = DateTime.Now.AddDays(-2)
                        }
                    };

                    var userBookings = new[]
                    {
                        new Booking
                        {
                            UserId = normalUser.Id,
                            ShowtimeId = showtimes[1].Id,
                            NumberOfTickets = 3,
                            BookingDate = DateTime.Now.AddDays(-3)
                        },
                        new Booking
                        {
                            UserId = normalUser.Id,
                            ShowtimeId = showtimes[3].Id,
                            NumberOfTickets = 2,
                            BookingDate = DateTime.Now.AddDays(-4)
                        }
                    };

                    context.Bookings.AddRange(adminBookings);
                    context.Bookings.AddRange(userBookings);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}