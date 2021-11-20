﻿using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectsByStatusCreatorIdTagIdSpecification : BaseSpecification<Project>
    {
        public ProjectsByStatusCreatorIdTagIdSpecification(string accountId)
            : base(p=>p.CreatorId == accountId)
        {
            AddInclude(p => p.Issues.SelectMany(i => i.Tags));
        }
        public ProjectsByStatusCreatorIdTagIdSpecification
            (string accountId,
            int tagId)
            : base(p => p.CreatorId == accountId && 
            p.Status == tagId)
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.Template);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Accounts);
            AddInclude(p => p.Roles);
            AddInclude(p => p.Issues);
            AddInclude(p => p.Statuses);
            AddInclude("Issues.Tags");
            AddInclude("Issues.Status");
        }
    }
}
