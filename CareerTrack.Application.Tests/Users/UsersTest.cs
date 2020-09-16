using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Tests.Users
{
    public class UsersTest
    {
        public CareerTrackDbContext db;

        public UsersTest()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>().
             UseInMemoryDatabase(databaseName: "CareerTrackUsers").Options;
            db = new CareerTrackDbContext(options);

            try
            {
                db.Roles.RemoveRange(db.Roles);
                db.Roles.AddRange(new[] {
                new Role {
                    Id = Guid.Parse("2BAF6E28-B1C9-42FE-953F-3B660BEFC6DA"),
                    Name = "StandardUser",
                    NormalizedName = "STANDARDUSER",
                    ConcurrencyStamp = "5855AE5D-281A-47B6-9A0C-180DA1BEEABC"
                    },
                new Role {
                   Id = Guid.Parse("3B246FA9-9B03-42B1-A06D-BFDE47AA6868"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "9C288E42-C458-408D-8091-BEF38D5396E3"
                    }
                });


                db.SaveChanges();
            }
            catch (ArgumentException)
            {
                //in case it was added the same Id
                db.Roles.RemoveRange(db.Roles);
            }
        }
    }
}
