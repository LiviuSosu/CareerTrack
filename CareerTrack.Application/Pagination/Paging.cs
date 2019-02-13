using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CareerTrack.Application.Pagination
{
    public static class Paging<T> where T : class
    {
        public static IList<T> GetPagedResponse(DbSet<T> list, PagingModel paggingModel)
        {
            var result = new List<T>();
            if (!string.IsNullOrWhiteSpace(paggingModel.QueryFilter))
            {
                //result = list.Where(x => x..ToLower().Contains(request.Pagination.QueryFilter.ToLower()));
            }
            return result;
        }
    }
}
