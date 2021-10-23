﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Core.Common;

namespace Bug.Data
{
    public class BugContextSeed
    {
        public static async Task SeedAsync(BugContext bugContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            try
            {
                if(!await bugContext.Projects.AnyAsync())
                {
                    await bugContext.Projects.AddRangeAsync(
                        GetPreconfiguredProjects());                   
                }
                if(!await bugContext.Accounts.AnyAsync())
                {
                    await bugContext.Accounts.AddRangeAsync(
                        GetPreconfiguredAccount());
                }
                if(!await bugContext.Workflows.AnyAsync())
                {
                    await bugContext.Workflows.AddRangeAsync(
                    GetPreconfiguredWorkflow());
                }
                if(!await bugContext.Tags.AnyAsync())
                {
                    await bugContext.Tags.AddRangeAsync(
                    GetPreconfiguredTag());
                }
                if(!await bugContext.Categories.AnyAsync())
                {
                    await bugContext.Categories.AddRangeAsync(
                    GetPreconfiguredCategory());
                }

                //var a = new Account("account4", "nameuuu", "pass3", "first3", "last3", "email3", DateTime.Now, "uri1",null);
                //a.AddProject("project4", "name4", DateTime.Now, DateTime.Now, "des4", "account3", "workflow1");
                //bugContext.Add(a);

                await bugContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var log = loggerFactory.CreateLogger<BugContextSeed>();
                log.LogError(ex.Message);
                throw;
            }
        }
        static IEnumerable<Project> GetPreconfiguredProjects()
        {           
            return new List<Project>()
            {
                new Project("project1","name1",DateTime.Now,DateTime.Now,"des1","account1","workflow1"),
                new Project("project2","name2",DateTime.Now,DateTime.Now,"des2","account2","workflow1"),
                new Project("project3","name3",DateTime.Now,DateTime.Now,"des3","account3","workflow1"),
            };
        }
        static IEnumerable<Account> GetPreconfiguredAccount()
        {
            return new List<Account>()
            {
                new Account("account1","name1","pass1","first1","last1","email1",DateTime.Now,"uri1",null),
                new Account("account2","name2","pass2","first2","last2","email2",DateTime.Now,"uri2",null),
                new Account("account3","name3","pass3","first3","last3","email3",DateTime.Now,"uri3",null)
            };
        }
        static IEnumerable<Workflow> GetPreconfiguredWorkflow()
        {
            return new List<Workflow>()
            {
                new Workflow("workflow1","name1","account1"),
                new Workflow("workflow2","name2","account2")
            };
        }
        static IEnumerable<Tag> GetPreconfiguredTag()
        {
            return new List<Tag>()
            {
                new Tag("Done",null,Bts.IssueCategory),
                new Tag("Open",null,Bts.IssueCategory)
            };
        }
        static IEnumerable<Category> GetPreconfiguredCategory()
        {
            return new List<Category>()
            {
                new Category("Account",null),
                new Category("Project",null),
                new Category("Issue",null),
                new Category("Workflow",null)
            };
        }
    }
}
