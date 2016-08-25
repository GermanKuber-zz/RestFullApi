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
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var user = await this._userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(UserMapper.Map(user));
                
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Post(UserViewModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var user = UserMapper.Map(model);

                var userUpdate = await this._userService.InserAsync(user);
                if (userUpdate.Status == ActionStatus.Created)
                    return Created(Request.RequestUri + "/" + userUpdate.Entity.Id
                        , UserMapper.Map(userUpdate.Entity));

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]UserViewModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var userDb = UserMapper.Map(model);

                var result = await _userService.UpdateAsync(userDb);
                if (result.Status == ActionStatus.Updated)
                    return Ok(UserMapper.Map(result.Entity));
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
        public async Task<IHttpActionResult> Patch(PatchUserViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var userDb = await _userService.GetByIdAsync(model.Id);
                if (userDb == null)
                {
                    return NotFound();
                }


                var userViewModel = UserMapper.Map(userDb);


                model.Model.ApplyTo(userViewModel);


                var result = await _userService.UpdateAsync(UserMapper.Map(userViewModel, userDb));

                if (result.Status == ActionStatus.Updated)
                {

                    var returnMapper = UserMapper.Map(result.Entity);
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
            
                var result = await _userService.DeleteAsync(id);

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
        [Route("users/{id}/Communitys")]
        public async Task<IHttpActionResult> GetCommunitys(int id)
        {
            try
            {
                //TODO: Paso 7 - 1 - Uri de recursos
                var user = await this._userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound();


                var results = user.Communitys?
                    .ToList()
                    .Select(CommunityMapper.Map);

                return Ok(results);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
