﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Tag : IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public ICollection<Status> Statuses { get; private set; }
        public ICollection<Issue> Issues { get; private set; }
        private Tag() { }
        public Tag(int id,
            string name,
            string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
