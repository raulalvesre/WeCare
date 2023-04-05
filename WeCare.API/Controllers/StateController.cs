using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
public class StateController : ControllerBase
{

    [HttpGet("api/brazilian-states")]
    public ActionResult<IEnumerable<State>> GetAll()
    {
        return Ok(Enum.GetValues(typeof(State)));
    }

}