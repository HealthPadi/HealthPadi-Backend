using HealthPadiWebApi.Data;
using HealthPadiWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                // Seed HealthyLivingTopics
                if (!_context.HealthyLivingTopics.Any())
                {
                    var topics = new List<string>
                    {
                        "How to Perform CPR", "Healthy Living Tips", "Eating Right", "Benefits of Regular Exercise",
                        "Importance of Hydration", "Stress Management Techniques", "Sleep Hygiene Practices",
                        "Balancing Work and Life", "Mental Health Awareness", "Healthy Snack Ideas", "Yoga Benefits",
                        "Meditation Techniques", "Healthy Meal Planning", "The Power of Positive Thinking",
                        "Maintaining a Healthy Weight", "Preventive Health Screenings", "Boosting Immunity Naturally",
                        "Avoiding Processed Foods", "Choosing Whole Grains", "Healthy Fats vs. Unhealthy Fats",
                        "Reducing Sugar Intake", "Understanding Nutrition Labels", "Importance of Fiber",
                        "Plant-Based Diet Benefits", "Mindful Eating Practices", "Healthy Aging Tips", "Cardiovascular Health",
                        "Strength Training Benefits", "Improving Flexibility", "Hydration and Skin Health", "Dealing with Anxiety",
                        "Managing Chronic Conditions", "Heart-Healthy Recipes", "Detoxification Practices", "Healthy Cooking Techniques",
                        "Superfoods and Their Benefits", "The Role of Vitamins and Minerals", "Healthy Eating on a Budget",
                        "Stress Reduction Strategies", "Importance of Regular Check-Ups", "Self-Care Routines",
                        "Building a Support System", "Avoiding Harmful Habits", "Setting Realistic Health Goals", "Understanding Metabolism",
                        "Sleep and Mental Health", "Nutritional Supplements", "The Importance of Sunlight", "Healthy Lifestyle Habits",
                        "Balancing Macronutrients", "Healthy Digestive System", "Managing Weight Loss", "Understanding Food Allergies",
                        "Detox Diets and Their Risks", "Maintaining Bone Health", "Healthy Heart Tips", "Mental Resilience Practices",
                        "Managing Blood Pressure", "Healthy Eating for Families", "The Impact of Caffeine", "Healthy Portion Sizes",
                        "Safe Exercise Practices", "Mindfulness Techniques", "Healthy Meal Prep", "The Benefits of Walking",
                        "Handling Emotional Eating", "The Role of Hydration in Fitness", "Natural Remedies for Common Ailments",
                        "Healthy Eating for Kids", "Understanding Nutritional Supplements", "The Impact of Screen Time on Health",
                        "Maintaining a Healthy Immune System", "Incorporating Movement into Daily Routine", "The Benefits of Group Exercise",
                        "Healthy Living for Seniors", "Dealing with Insomnia", "Healthy Aging Strategies", "The Role of Protein in Diet",
                        "Managing Healthy Blood Sugar Levels", "Healthy Eating for Pregnant Women", "The Importance of Breakfast",
                        "Practical Tips for a Healthier Lifestyle", "The Benefits of Regular Physical Activity"
                    };

                    var healthyLivingTopics = topics.Select(topic => new HealthyLivingTopic
                    {
                        HealthyLivingTopicId = Guid.NewGuid(),
                        Topic = topic
                    }).ToList();

                    _context.HealthyLivingTopics.AddRange(healthyLivingTopics);
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
