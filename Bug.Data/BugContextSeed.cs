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
                new Project("project1","name1","code1","type1",DateTime.Now,DateTime.Now,DateTime.Now,"des1",null,null,"account1"),
                new Project("project2","name2","code2","type2",DateTime.Now,DateTime.Now,DateTime.Now,"des2",null,null,"account2"),
                new Project("project3","name3","code3","type3",DateTime.Now,DateTime.Now,DateTime.Now,"des3",null,null,"account3")
            };
        }
        static IEnumerable<Account> GetPreconfiguredAccount()
        {
            return new List<Account>()
            {
                new Account("account1","name1","pass1","first1","last1","email1",DateTime.Now,null,"uri1",null),
                new Account("account2","name2","pass2","first2","last2","email2",DateTime.Now,null,"uri2",null),
                new Account("account3","name3","pass3","first3","last3","email3",DateTime.Now,null,"uri3",null)
            };
        }
        static IEnumerable<Tag> GetPreconfiguredTag()
        {
            return new List<Tag>()
            {
                new Tag("Open",null,Bts.IssueTag),
                new Tag("Close",null,Bts.IssueTag),
                new Tag("Open",null,Bts.ProjectTag)
            };
        }
        static IEnumerable<Category> GetPreconfiguredCategory()
        {
            return new List<Category>()
            {
                new Category("Account",null),
                new Category("Project",null),
                new Category("Issue",null),
            };
        }
    }
}
