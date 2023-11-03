using EgyptianEscape.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Data.Configurations
{
    public class VillaConfiguration : IEntityTypeConfiguration<Villa>
    {
        public void Configure(EntityTypeBuilder<Villa> builder)
        {
            builder.HasData(new Villa
            {
                Id = 1,
                Name = "Royal Villa ",
                ImageUrl = "https://placehold.co/600x400",
                Occupancy = 4,
                Description = "Villa 1 Description",
                Price = 200,
                Area = 500
            },
            new Villa
            {
                Id = 2,
                Name = "Premium Pool Villa ",
                ImageUrl = "https://placehold.co/600x400",
                Occupancy = 5,
                Description = "Villa 1 Description",
                Price = 300,
                Area = 7000
            },
            new Villa
            {
                Id = 3,
                Name = "Luxury Pool Villa ",
                ImageUrl = "https://placehold.co/600x400",
                Occupancy = 6,
                Description = "Villa 1 Description",
                Price = 400,
                Area = 1000
            });
        }
    }
}
