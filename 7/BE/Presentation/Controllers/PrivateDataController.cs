using HW7.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateDataController : ControllerBase
    {
        private readonly GetPrivateDataUseCase _getPrivateDataUseCase;

        public PrivateDataController(GetPrivateDataUseCase getPrivateDataUseCase)
        {
            _getPrivateDataUseCase = getPrivateDataUseCase;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetPrivateData()
        {
            var data = _getPrivateDataUseCase.Execute();
            return Ok(new { Data = data });
        }
    }
}
