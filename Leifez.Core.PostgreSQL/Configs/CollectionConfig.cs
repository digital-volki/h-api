﻿using Leifez.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leifez.Core.PostgreSQL.Configs
{
    public class CollectionConfig : IEntityTypeConfiguration<DbCollection>
    {
        public void Configure(EntityTypeBuilder<DbCollection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(100);
        }
    }
}
