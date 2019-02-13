using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Tests
{
    public class TestBase
    {
        public CareerTrackDbContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<CareerTrackDbContext>();
            if (useSqlLite)
            {
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new CareerTrackDbContext(builder.Options);
            if (useSqlLite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
