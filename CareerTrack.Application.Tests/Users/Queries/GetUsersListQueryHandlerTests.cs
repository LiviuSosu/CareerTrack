using CareerTrack.Application.Pagination;
using CareerTrack.Application.Users.Queries.GetUsersList;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Queries
{
    public class GetUsersListQueryHandlerTests
    {
        [Fact]
        public async Task GetUsersTest2()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
               .UseInMemoryDatabase(databaseName: "CareerTrackUsers")
               .Options;

            CareerTrackDbContext db = new CareerTrackDbContext(options);

            db.Users.AddRange(new[] {
                new User { Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B") , UserName = "AdamCogan" },
                new User { Id = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B"), UserName = "JasonTaylor" },
                new User { Id = Guid.Parse("FEA44EA2-1D4C-49BB-92A0-1AD6899CA220"), UserName = "BrendanRichards" },
            });

            db.SaveChanges();

            var sut = new GetUsersListQueryHandler(db);

            var pg = new PaginationModel
            {
                Field = "Username"
            };
            var ord = new Order();
            pg.Order = ord;
            pg.PageNumber = 1;
            pg.PageSize = 2;
            pg.QueryFilter = "d";
            var result = await sut.Handle(new GetUsersListQuery() { Pagination = pg }, CancellationToken.None);

            result.ShouldBeOfType<UsersListViewModel>();

            result.Users.Count.ShouldBe(2);
        }
    }
}
