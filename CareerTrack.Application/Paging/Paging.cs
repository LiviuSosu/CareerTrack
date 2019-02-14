using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CareerTrack.Application.Paging
{
    public static class Paging<T> where T : class
    {
        public static IList<T> GetPagedResponse(DbSet<T> list, PagingModel paggingModel)
        {
            throw new NotImplementedException();
        }
    }
}
