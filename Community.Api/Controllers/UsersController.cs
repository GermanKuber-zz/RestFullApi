using System;
using System.Threading.Tasks;
using System.Web.Http;
using Community.Core.Interfaces.Services;
using Community.Core.Results;
using Community.Mapper;
using Community.ViewModel.Request;

namespace Community.APi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return Ok();
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
    }

}
