using Entities.Models;
using Entities.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Rabrune.Data;
using Entities.Models.Feed;

namespace Data.Repositories
{
    public interface IHomeRepository
    {
        //public Task<IEnumerable<HomePageResponse>> Get(int Id);
        //public Task<IEnumerable<HomePageResponse>> Post(FeedRequest request);
    }

    public class HomeRepository : IHomeRepository
    {
        private readonly ILogger _logger;
        private readonly IUniversal _repo;

        public HomeRepository(string connectionString)
        {
            _repo = new Universal(connectionString);

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddConsole();
            });

            _logger = loggerFactory.CreateLogger<HomeRepository>();
        }

        //public async Task<IEnumerable<HomePageResponse>> Get(int Id)
        //{
        //    try
        //    {               
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError($"LoginRequest", ex.Message, request);
        //        throw ex;
        //    }
        //}

        //public Task<IEnumerable<HomePageResponse>> Post(FeedRequest request)
        //{
        //    try
        //    {
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        //private HomePageResponse Parse(DataRow row)
        //{
        //    HomePageResponse model = new HomePageResponse();

        //    //model.PostId = Utils.IntParse(row["Id"]);
        //    //model.UserId = Utils.IntParse(row["user_id"]);
        //    //model.UserName = Utils.StringTryParse(row["user_name"]);
        //    //model.FeedText = Utils.StringTryParse(row["feed_text"]);
        //    //model.LikeQuantity = Utils.IntParse(row["like_quantity"]);
        //    //model.CreatedAt = Utils.DateTimeParse(row["created_at"]);
        //    //model.Comment = Utils.StringTryParse(row["comment"]);
        //    //model.CommentQuantity = Utils.IntParse(row["comment_quantity"]);

        //    return model;
        //}
    }
}
