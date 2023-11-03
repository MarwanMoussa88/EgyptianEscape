using EgyptianEscape.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Data.Configurations
{
    internal class VillaNumberConfigurations : IEntityTypeConfiguration<VillaNumber>
    {
        public void Configure(EntityTypeBuilder<VillaNumber> builder)
        {
            builder.HasData(new VillaNumber
            {
                Villa_Number = 101,
                VillaId = 1
            },
            new VillaNumber
            {
                Villa_Number = 102,
                VillaId = 1
            }, new VillaNumber
            {
                Villa_Number = 103,
                VillaId = 1
            },
            new VillaNumber
            {
                Villa_Number = 201,
                VillaId = 2
            },
            new VillaNumber
            {
                Villa_Number = 202,
                VillaId = 2
            },
            new VillaNumber
            {
                Villa_Number = 203,
                VillaId = 2
            },
            new VillaNumber
            {
                Villa_Number = 301,
                VillaId = 3
            },
            new VillaNumber
            {
                Villa_Number = 302,
                VillaId = 3
            },
            new VillaNumber
            {
                Villa_Number = 303,
                VillaId = 3
            });
        }
    }
}
