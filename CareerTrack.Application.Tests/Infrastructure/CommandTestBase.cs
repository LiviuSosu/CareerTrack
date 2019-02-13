using CareerTrack.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly CareerTrackDbContext _context;

        public CommandTestBase()
        {
            _context = CareerTrackContextFactory.Create();
        }

        public void Dispose()
        {
            CareerTrackContextFactory.Destroy(_context);
        }
    }
}
