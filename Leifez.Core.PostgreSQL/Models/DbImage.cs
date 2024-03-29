﻿using System;
using System.Collections.Generic;

namespace Leifez.Core.PostgreSQL.Models
{
    public class DbImage
    {
        public string Guid { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<DbTag> Tags { get; set; } = new List<DbTag>();
        public List<DbCollection> Collections { get; set; } = new List<DbCollection>();
    }
}