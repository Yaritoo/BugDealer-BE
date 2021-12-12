﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class ProjectPutDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset? RecentDate { get; set; }
        public string AvatarUri { get; set; }
        public string ProjectType { get; set; }
        public string Description { get; set; }
        public int? State { get; set; }
        public int? TemplateId { get; set; }
        public string DefaultAssigneeId { get; set; }
        public string CreatorId { get; set; }
        public List<TagNormalDto> Tags { get; set; }
        public List<AccountNormalDto> Accounts { get; set; }
        public List<RoleNormalDto> Roles { get; set; }
        public List<StatusNormalDto> Statuses { get; set; }

        // accountId for delete member from project case
        public string AccountId { get; set; }
    }
}
