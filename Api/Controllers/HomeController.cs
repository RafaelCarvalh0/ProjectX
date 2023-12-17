using Data.Repositories;
using Entities.Models;
using Entities.Models.Feed;
using Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class HomeController : ControllerBase
    //{
    //    private readonly ILogger _logger;
    //    private readonly IHomeRepository _repo;

    //    public HomeController(IOptions<Models.ConnectionStrings> config, ILogger<HomeController> logger, IHttpContextAccessor accessor)
    //    {
    //        _logger = logger;
    //        _repo = new HomeRepository(config.Value.AWS);
    //    }

    //    // GET: api/Home/Get
    //    /// <summary>
    //    /// Recupera todos os posts da tela Home baseado no ID da requisição.
    //    /// </summary>
    //    /// <param name="Id">Filtros</param>
    //    /// <returns>Retorna um array de objetos do tipo HomePageResponse</returns>
    //    /// <response code="200">Operação realizada com exito</response>
    //    /// <response code="500">Ocorreu um erro interno</response>
    //    /// <response code="401">Usuário não autenticado</response>
    //    [HttpGet]
    //    [Route("[action]/{Id}")]
    //    public async Task<ActionResult<IEnumerable<HomePageResponse>>> Get(int Id)
    //    {
    //        try
    //        {
    //            var retorno = await _repo.Get(Id);

    //            if (retorno == null)
    //            {
    //                return new EmptyResult();
    //            }

    //            return Ok(retorno);
    //        }
    //        catch (Exception ex)
    //        {
    //           // _logger.LogError($"LoginRequest", request);

    //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //        }
    //    }

    //    // POST: api/Home/PostFeed
    //    /// <summary>
    //    /// Cria uma postagem no feed de notícias, baseado nos parâmetros enviados na requisição.
    //    /// </summary>
    //    /// <param name="request">Filtros</param>
    //    /// <returns>Retorna um array de objetos atualizados do tipo HomePageResponse</returns>
    //    /// <response code="200">Operação realizada com exito</response>
    //    /// <response code="500">Ocorreu um erro interno</response>
    //    /// <response code="401">Usuário não autenticado</response>
    //    [HttpPost]
    //    [Route("[action]")]
    //    public async Task<ActionResult<IEnumerable<HomePageResponse>>> PostFeed(FeedRequest request)
    //    {
    //        try
    //        {
    //            var retorno = await _repo.Post(request);

    //            if (retorno == null)
    //            {
    //                return new EmptyResult();
    //            }

    //            return Ok(retorno);
    //        }
    //        catch (Exception ex)
    //        {
    //            // _logger.LogError($"LoginRequest", request);

    //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //        }
    //    }
    //}
}
