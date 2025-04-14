using GameApi.Models;
using GameApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ButtonController : ControllerBase
    {
        private readonly MongoDBService _db;

        public ButtonController(MongoDBService db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterClick([FromBody] Button click)
        {
            click.ClickTime = DateTime.UtcNow;
            await _db.SaveButtonClick(click);
            return Ok(new { success = true, clickId = click.Id });
        }
    }
}
