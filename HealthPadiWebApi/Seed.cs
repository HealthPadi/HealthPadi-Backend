using HealthPadiWebApi.Data;
using HealthPadiWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi
{
    public class Seed
    {
        private readonly HealthPadiDataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public Seed(HealthPadiDataContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataContext()
        {
            try
            {
                // Seed Roles
                var roles = new List<IdentityRole<Guid>>
                {
                    new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" },
                    new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Guest", NormalizedName = "GUEST" }
                };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role.Name))
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }

                // Seed Users
                if (!_context.Users.Any())
                {
                    var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Alice",
                        Lastname = "Smith",
                        Email = "alice.smith@example.com",
                        UserName= "alice.smith@example.com"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Bob",
                        Lastname = "Johnson",
                        Email = "bob.johnson@example.com",
                       UserName= "bob.johnson@example.com"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Charlie",
                        Lastname = "Brown",
                        Email = "charlie.brown@example.com",
                        UserName= "charlie.brown@example.com"
                    }
                };

                    foreach (var user in users)
                    {
                        var existingUser = await _userManager.FindByEmailAsync(user.Email);
                        if (existingUser == null)
                        {
                            var result = await _userManager.CreateAsync(user, "Admin123@");
                            if (result.Succeeded)
                            {
                                if (user.Email == "alice.smith@example.com")
                                    await _userManager.AddToRoleAsync(user, "User");
                                else if (user.Email == "bob.johnson@example.com")
                                    await _userManager.AddToRoleAsync(user, "Admin");
                                else if (user.Email == "charlie.brown@example.com")
                                    await _userManager.AddToRoleAsync(user, "Guest");
                            }
                        }
                        else
                        {
                            // Optionally update existing user details if needed
                            existingUser.Firstname = user.Firstname;
                            existingUser.Lastname = user.Lastname;
                            existingUser.Email = user.Email;
                            await _userManager.UpdateAsync(existingUser);
                        }
                    }
                }

                // Seed Reports
                if (!_context.Reports.Any())
                {
                    Console.WriteLine("Seeding Reports...");
                    var alice = await _userManager.FindByEmailAsync("alice.smith@example.com");
                    var bob = await _userManager.FindByEmailAsync("bob.johnson@example.com");

                    if (alice != null && bob != null)
                    {
                        var reports = new List<Report>
                    {
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = alice.Id,
                            Location = "Los Angeles",
                            Content = "Cholera outbreak in Los Angeles: Increasing cases and severe health risks."
                        },
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = bob.Id,
                            Location = "New York",
                            Content = "Measles cases rising in New York: Urgent need for vaccinations."
                        }
                    };

                        _context.Reports.AddRange(reports);
                        await _context.SaveChangesAsync();
                    }
                }
                // Seed Feeds
                if (!_context.Feeds.Any())
                {
                    var feeds = new List<Feed>
                    {
                        new Feed
                        {
                            FeedId = Guid.NewGuid(),
                            FeedContent = "10 Tips to Prevent Malaria: Use insect repellent, sleep under mosquito nets, and avoid standing water."
                        },
                        new Feed
                        {
                            FeedId = Guid.NewGuid(),
                            FeedContent = "Wellness Tip: Stay hydrated and maintain a balanced diet to boost your immune system."
                        },
                        new Feed
                        {
                            FeedId = Guid.NewGuid(),
                            FeedContent = "Avoid Flu: Get vaccinated, wash hands regularly, and avoid close contact with sick individuals."
                        }
                    };

                    _context.Feeds.AddRange(feeds);
                    await _context.SaveChangesAsync();
                }

                // Seed HealthUpdates
                if (!_context.HealthUpdates.Any())
                {
                    var healthUpdates = new List<HealthUpdate>
                    {
                        new HealthUpdate
                        {
                            HealthUpdateId = Guid.NewGuid(),
                            Content = "Cholera is predominant in Los Angeles: Water contamination is a major concern."
                        },
                        new HealthUpdate
                        {
                            HealthUpdateId = Guid.NewGuid(),
                            Content = "Measles outbreak in New York: Hospitals are at capacity, and awareness campaigns are underway."
                        },
                        new HealthUpdate
                        {
                            HealthUpdateId = Guid.NewGuid(),
                            Content = "Dengue cases in Chicago: Public health advisory issued for residents to eliminate mosquito breeding sites."
                        }
                    };

                    _context.HealthUpdates.AddRange(healthUpdates);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }
}
