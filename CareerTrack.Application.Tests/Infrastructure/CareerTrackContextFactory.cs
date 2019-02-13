using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Tests.Infrastructure
{
    public class CareerTrackContextFactory
    {
        public static CareerTrackDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CareerTrackDbContext(options);

            context.Database.EnsureCreated();

            context.Users.AddRange(new[] {
                 new User { Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B") , UserName = "AdamCogan" },
                new User { Id = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B"), UserName = "JasonTaylor" },
                new User { Id = Guid.Parse("FEA44EA2-1D4C-49BB-92A0-1AD6899CA220"), UserName = "BrendanRichards" },
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(CareerTrackDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
