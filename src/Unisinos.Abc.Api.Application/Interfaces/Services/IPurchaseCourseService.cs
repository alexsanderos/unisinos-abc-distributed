using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Api.Application.Interfaces.Services
{
    public interface IPurchaseCourseService
    {
        Task PurchaseCreditCourse(PurchaseCreditCourseCommand command);
    }
}
