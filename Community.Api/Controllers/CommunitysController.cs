using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Community.Core.Interfaces.Services;
using Community.Core.Results;
using Community.Mapper;
using Community.ViewModel.Request;

namespace Community.APi.Controllers
{
    [RoutePrefix("api")]
    public class CommunitysController : ApiController
    {
        private readonly ICommunityService _communityService;


        public CommunitysController(ICommunityService communityService)
        {
            _communityService = communityService;

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