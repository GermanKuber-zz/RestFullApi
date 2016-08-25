using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using Community.Core.Interfaces.Services;
using Community.Core.Results;
using Community.Helper;
using Community.Mapper;
using Community.ViewModel.Request;

namespace Community.APi.Controllers
{
    [RoutePrefix("api")]
    public class CommunitysController : ApiController
    {
        private readonly ICommunityService _communityService;
        const int MaxPageSize = 10;

        public CommunitysController(ICommunityService communityService)
        {
            _communityService = communityService;

        }
        [Route("Communitys", Name = "CommunitysList")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string sort = "id", string fields = null,int page = 1, int pageSize = MaxPageSize)
        {
            try
            {
                //TODO: Paso 10 - 2 - Seleccionar Campos individuales - Se agrega field los campos se pasan separados por coma

                //Ejemplo : /api/Communitys?fields=name'
                //Ejemplo : /api/Communitys?fields=name%2Cid' (se pasan name y id)
                //Ejemplo : /api/Communitys?fields=tags' (Propiedad completa de tag)

                var users = await this._communityService.GetAllAsync();

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                    lstOfFields = fields.ToLower().Split(',').ToList();


                // Limito el maximo
                if (pageSize > MaxPageSize)
                    pageSize = MaxPageSize;


                // calculo paginas
                var totalCount = users.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("CommunitysList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields=fields
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("CommunitysList",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields
                    }) : "";


                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                   Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


                return Ok(users
                          .ApplySort(sort)
                          .Skip(pageSize * (page - 1))
                          .Take(pageSize)
                          .ToList()
                          .Select(x=> CommunityMapper.MapObject(CommunityMapper.Map(x), lstOfFields)));

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var data = await this._communityService.GetByIdAsync(id);
                if (data == null)
                    return NotFound();

                return Ok(CommunityMapper.Map(data));

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Post(CommunityViewModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var user = CommunityMapper.Map(model);

                var userUpdate = await this._communityService.InserAsync(user);
                if (userUpdate.Status == ActionStatus.Created)
                    return Created(Request.RequestUri + "/" + userUpdate.Entity.Id
                        , CommunityMapper.Map(userUpdate.Entity));

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]CommunityViewModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var db = CommunityMapper.Map(model);

                var result = await _communityService.UpdateAsync(db);
                if (result.Status == ActionStatus.Updated)
                    return Ok(CommunityMapper.Map(result.Entity));
                else if (result.Status == ActionStatus.NotFound)
                    return NotFound();

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        [HttpPatch]
        public async Task<IHttpActionResult> Patch(PatchCommunityViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var db = await _communityService.GetByIdAsync(model.Id);
                if (db == null)
                    return NotFound();


                var viewModel = CommunityMapper.Map(db);


                model.Model.ApplyTo(viewModel);


                var result = await _communityService.UpdateAsync(CommunityMapper.Map(viewModel, db));

                if (result.Status == ActionStatus.Updated)
                {

                    var returnMapper = CommunityMapper.Map(result.Entity);
                    return Ok(returnMapper);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
            
                var result = await _communityService.DeleteAsync(id);

                if (result.Status == ActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == ActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }
        [Route("communitys/{id}/tags")]
        public async Task<IHttpActionResult> GetCommunitys(int id)
        {
            try
            {
              
                var db = await this._communityService.GetByIdAsync(id);
                if (db == null)
                    return NotFound();

               var results = db.Tags?
                    .ToList()
                    .Select(x =>
                    {
                        x.Communitys = null;
                        return CommunityTagMapper.Map(x);
                    });

                return Ok(results);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}