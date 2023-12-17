using Entities.Models.Feed;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Repositories;
using Microsoft.Extensions.Logging;
using Rabrune.Data;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repositories
{
    public interface IFeedRepository
    {
        public Task<IEnumerable<FeedResponse>> Get(int Id);
        public Task<IEnumerable<FeedCommentsResponse>> GetComments(int feedId);
        //public Task<IEnumerable<FeedResponse>> Post(FeedRequest request);
    }

    public class FeedRepository : IFeedRepository
    {
        private readonly ILogger _logger;
        private readonly IUniversal _repo;

        public FeedRepository(string connectionString)
        {
            _repo = new Universal(connectionString);

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddConsole();
            });

            _logger = loggerFactory.CreateLogger<FeedRepository>();
        }

        public async Task<IEnumerable<FeedResponse>> Get(int Id)
        {
            try
            {
                var retorno = new List<FeedResponse>();

                var result = await _repo.ExecuteDataTable(
                command: "proc_feed_get",
                type: CommandType.StoredProcedure,
                new SqlParameter(parameterName: "@p_id", value: Utils.DBNullParse(Id))
            );

                if (result.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        retorno.Add(
                            Parse(row)
                        );
                    }

                    return retorno;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"LoginRequest", ex.Message, request);
                throw ex;
            }
        }

        public async Task<IEnumerable<FeedCommentsResponse>> GetComments(int feedId)
        {
            try
            {
                var retorno = new List<FeedCommentsResponse>();

                var result = await _repo.ExecuteDataTable(
                command: "proc_feed_comments_get",
                type: CommandType.StoredProcedure,
                new SqlParameter(parameterName: "@p_feed_id", value: Utils.DBNullParse(feedId))
                );

                if (result.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        retorno.Add(
                            ParseComments(row)
                        );
                    }

                    return retorno;
                }

                return retorno;

            }
            catch (Exception ex)
            {
                //_logger.LogError($"postId", ex.Message, postId);
                throw;
            }
        }



        //public Task<IEnumerable<FeedResponse>> Post(FeedRequest request)
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

        private FeedResponse Parse(DataRow row)
        {
            FeedResponse model = new FeedResponse();

            model.PostId = Utils.IntParse(row["Id"]);
            model.UserId = Utils.IntParse(row["user_id"]);
            model.UserName = Utils.StringTryParse(row["user_name"]);
            model.FeedText = Utils.StringTryParse(row["feed_text"]);
            model.CreatedAt = Utils.DateTimeParse(row["created_at"]);
            model.CommentCount = Utils.IntTryParse(row["comment_count"]);

            return model;
        }

        private FeedCommentsResponse ParseComments(DataRow row)
        {
            FeedCommentsResponse model = new FeedCommentsResponse
            {
                FeedId = Utils.IntParse(row["feed_id"]),
                FeedComment = Utils.StringTryParse(row["feed_comment"]),
                FeedLikes = Utils.IntTryParse(row["feed_likes"]),
                CreatedAt = Utils.DateTimeParse(row["created_at"]),
            };

            return model;
        }
    }
}
