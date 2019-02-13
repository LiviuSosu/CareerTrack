using AutoMapper;
using CareerTrack.Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CareerTrack.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public CareerTrackDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = CareerTrackContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            CareerTrackContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
