using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListViewModel>
    {
        private readonly CareerTrackDbContext _context;

        public GetUsersListQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<UsersListViewModel> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var vm = new UsersListViewModel
            {
                Users = await _context.Users.Select(c =>
                    new UserLookupModel
                    {
                        Id = c.Id,
                        Name = c.UserName
                    }).ToListAsync(cancellationToken)
            };

            return vm;
        }
    }
}
