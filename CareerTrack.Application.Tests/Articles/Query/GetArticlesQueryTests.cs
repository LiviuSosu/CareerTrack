using CareerTrack.Application.Handlers.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticlesQueryTests : ArticlesTest
    {
        [Fact]
        public async Task GetArticlesListQueryHandlerTest_Succes()
        {
            var sut = new GetArticlesListQueryHandler(db);

            var pagingModel = new PagingModel
            {
                Field = "Username"
            };
            var order = new Order();
            pagingModel.Order = order;
            pagingModel.PageNumber = 1;
            pagingModel.PageSize = 2;

            var getArticleQuery = new GetArticlesListQuery(pagingModel);

            var result = await sut.Handle(getArticleQuery, CancellationToken.None);

            Assert.IsType<ArticlesListViewModel>(result);
            Assert.NotNull(result);
            Assert.Equal(2, result.Articles.Count);
        }
    }
}
