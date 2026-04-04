using Microsoft.AspNetCore.Identity;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "Faculty", "Student" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@vgc.ie";
        var adminPassword = "Admin123!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        var facultyEmail = "faculty@vgc.ie";
        var facultyPassword = "Faculty123!";

        var facultyUser = await userManager.FindByEmailAsync(facultyEmail);

        if (facultyUser == null)
        {
            facultyUser = new IdentityUser
            {
                UserName = facultyEmail,
                Email = facultyEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(facultyUser, facultyPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(facultyUser, "Faculty");
            }
        }

        var student1Email = "student1@vgc.ie";
        var student1Password = "Student123!";

        var student1User = await userManager.FindByEmailAsync(student1Email);

        if (student1User == null)
        {
            student1User = new IdentityUser
            {
                UserName = student1Email,
                Email = student1Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(student1User, student1Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(student1User, "Student");
            }
        }

        var student2Email = "student2@vgc.ie";
        var student2Password = "Student123!";

        var student2User = await userManager.FindByEmailAsync(student2Email);

        if (student2User == null)
        {
            student2User = new IdentityUser
            {
                UserName = student2Email,
                Email = student2Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(student2User, student2Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(student2User, "Student");
            }
        }

        if (facultyUser != null && !context.FacultyProfiles.Any(f => f.IdentityUserId == facultyUser.Id))
        {
            context.FacultyProfiles.Add(new FacultyProfile
            {
                IdentityUserId = facultyUser.Id,
                Name = "Main Faculty",
                Email = facultyEmail,
                Phone = "0850000001"
            });
        }

        if (student1User != null && !context.StudentProfiles.Any(s => s.IdentityUserId == student1User.Id))
        {
            context.StudentProfiles.Add(new StudentProfile
            {
                IdentityUserId = student1User.Id,
                Name = "Student One",
                Email = student1Email,
                Phone = "0850000002",
                Address = "Dublin, Ireland",
                StudentNumber = "STU001"
            });
        }

        if (student2User != null && !context.StudentProfiles.Any(s => s.IdentityUserId == student2User.Id))
        {
            context.StudentProfiles.Add(new StudentProfile
            {
                IdentityUserId = student2User.Id,
                Name = "Student Two",
                Email = student2Email,
                Phone = "0850000003",
                Address = "Cork, Ireland",
                StudentNumber = "STU002"
            });
        }

        if (!context.Branches.Any())
        {
            context.Branches.AddRange(
                new Branch
                {
                    Name = "Dublin Branch",
                    Address = "Dublin, Ireland"
                },
                new Branch
                {
                    Name = "Cork Branch",
                    Address = "Cork, Ireland"
                },
                new Branch
                {
                    Name = "Galway Branch",
                    Address = "Galway, Ireland"
                }
            );
        }

        await context.SaveChangesAsync();
    }
}