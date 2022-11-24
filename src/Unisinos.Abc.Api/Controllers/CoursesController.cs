using Microsoft.AspNetCore.Mvc;
using Unisinos.Abc.Api.Application.Interfaces.Services;
using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IPurchaseCourseService _purchaseCourseService;

        private readonly ILogger<CoursesController> _logger;

        public CoursesController(
            IPurchaseCourseService purchaseCourseService,
            ILogger<CoursesController> logger)
        {
            _purchaseCourseService = purchaseCourseService;
            _logger = logger;
        }

        [HttpPost("Purchases")]
        public ActionResult<PurchaseCourseCreditResponse> PurchaseCreditCourse([FromBody] PurchaseCourseCreditRequest request)
        {
            var command = new PurchaseCreditCourseCommand()
            {
                CorrelationId = Guid.NewGuid(),
                StudentCPF = request.StudentCPF,
                StudentName = request.StudentName,
                NumberInstallments = request.NumberInstallments,
                TotalValue = request.TotalValue
            };
            _purchaseCourseService.PurchaseCreditCourse(command);

            return Ok(new PurchaseCourseCreditResponse(command.CorrelationId));
        }
    }
}