using ProjectManagement.API.Models;
using ProjectManagement.API.Models.Enums;

namespace ProjectManagement.API.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Projects.Any())
                return;

            var projects = new[]
            {
                new Project
                {
                    Name = "Website Redesign",
                    Description = "Complete redesign of company website with modern UI/UX",
                    CreatedAt = DateTime.Now.AddDays(-30)
                },
                new Project
                {
                    Name = "Mobile App Development",
                    Description = "Develop cross-platform mobile application for iOS and Android",
                    CreatedAt = DateTime.Now.AddDays(-20)
                },
                new Project
                {
                    Name = "Database Migration",
                    Description = "Migrate legacy database to new cloud infrastructure",
                    CreatedAt = DateTime.Now.AddDays(-10)
                }
            };

            context.Projects.AddRange(projects);
            context.SaveChanges();

            var tasks = new[]
            {
                // Website Redesign Tasks
                new ProjectTask
                {
                    ProjectId = projects[0].Id,
                    Title = "Design Homepage Mockup",
                    Description = "Create wireframes and mockups for the new homepage design",
                    Status = Models.Enums.TaskStatus.Done,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-25)
                },
                new ProjectTask
                {
                    ProjectId = projects[0].Id,
                    Title = "Implement Responsive Layout",
                    Description = "Code the responsive CSS for mobile and desktop views",
                    Status = Models.Enums.TaskStatus.InProgress,
                    Priority = TaskPriority.Medium,
                    CreatedAt = DateTime.Now.AddDays(-20)
                },
                new ProjectTask
                {
                    ProjectId = projects[0].Id,
                    Title = "Setup Contact Form",
                    Description = "Create and integrate contact form with email functionality",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Low,
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new ProjectTask
                {
                    ProjectId = projects[0].Id,
                    Title = "Optimize Website Performance",
                    Description = "Compress images and minify CSS/JS files",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Medium,
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new ProjectTask
                {
                    ProjectId = projects[0].Id,
                    Title = "SEO Implementation",
                    Description = "Add meta tags, structured data, and improve page speed",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-5)
                },

                // Mobile App Tasks
                new ProjectTask
                {
                    ProjectId = projects[1].Id,
                    Title = "Setup Development Environment",
                    Description = "Configure React Native development environment and tools",
                    Status = Models.Enums.TaskStatus.Done,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-18)
                },
                new ProjectTask
                {
                    ProjectId = projects[1].Id,
                    Title = "Create User Authentication",
                    Description = "Implement login/register functionality with JWT tokens",
                    Status = Models.Enums.TaskStatus.InProgress,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-12)
                },
                new ProjectTask
                {
                    ProjectId = projects[1].Id,
                    Title = "Design App Icons",
                    Description = "Create app icons for iOS and Android platforms",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Medium,
                    CreatedAt = DateTime.Now.AddDays(-8)
                },
                new ProjectTask
                {
                    ProjectId = projects[1].Id,
                    Title = "Implement Push Notifications",
                    Description = "Setup Firebase for push notifications on both platforms",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Medium,
                    CreatedAt = DateTime.Now.AddDays(-6)
                },
                new ProjectTask
                {
                    ProjectId = projects[1].Id,
                    Title = "Create User Profile Screen",
                    Description = "Design and implement user profile management",
                    Status = Models.Enums.TaskStatus.InProgress,
                    Priority = TaskPriority.Low,
                    CreatedAt = DateTime.Now.AddDays(-4)
                },
                new ProjectTask
                {
                    ProjectId = projects[1].Id,
                    Title = "App Store Submission",
                    Description = "Prepare and submit app to iOS App Store and Google Play",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-2)
                },

                // Database Migration Tasks
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Analyze Current Database Schema",
                    Description = "Document existing database structure and relationships",
                    Status = Models.Enums.TaskStatus.Done,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-9)
                },
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Setup Cloud Database Instance",
                    Description = "Configure new database instance on AWS RDS",
                    Status = Models.Enums.TaskStatus.InProgress,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Create Migration Scripts",
                    Description = "Write SQL scripts to migrate data safely",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Medium,
                    CreatedAt = DateTime.Now.AddDays(-3)
                },
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Test Data Integrity",
                    Description = "Verify all data migrated correctly and run integrity checks",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddDays(-1)
                },
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Setup Database Backup",
                    Description = "Configure automated daily backups for the new database",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Medium,
                    CreatedAt = DateTime.Now.AddHours(-12)
                },
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Performance Optimization",
                    Description = "Optimize queries and add proper indexing",
                    Status = Models.Enums.TaskStatus.Todo,
                    Priority = TaskPriority.Low,
                    CreatedAt = DateTime.Now.AddHours(-6)
                },
                new ProjectTask
                {
                    ProjectId = projects[2].Id,
                    Title = "Update Connection Strings",
                    Description = "Update all applications to use new database connection",
                    Status = Models.Enums.TaskStatus.InProgress,
                    Priority = TaskPriority.High,
                    CreatedAt = DateTime.Now.AddHours(-2)
                }
            };

            context.ProjectTasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}