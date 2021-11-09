﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.API.Dto
{
    public class StatusNormalDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Account> Accounts { get; set; }
    }
}