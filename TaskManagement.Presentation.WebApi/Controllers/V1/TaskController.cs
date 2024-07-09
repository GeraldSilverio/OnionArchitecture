using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Core.Application.Features.Task.Commands;
using TaskManagement.Core.Application.Features.Task.Queries;

namespace TaskManagement.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Controlador para el mantenimiento de tareas.")]
    public class TaskController : BaseApiController
    {
        public TaskController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
        Summary = "Crear una nueva tarea")]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            try
            {
                var task = await Mediator.Send(command);
                return task.Data != 0 ? StatusCode(StatusCodes.Status201Created, task) : StatusCode(StatusCodes.Status400BadRequest, task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }

        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(
        Summary = "Obtener todas las tareas del usuario")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await Mediator.Send(new GetAllTasksQuery());
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Obtener una tarea del ususario por su ID")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var task = await Mediator.Send(new GetTaskByIdQuery { Id = id });
                return Ok(task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Eliminar una tarea mediante su ID")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTaskCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = "Actualizar una tarea")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskCommand updateTaskCommand)
        {
            try
            {
                updateTaskCommand.Id = id;
                await Mediator.Send(updateTaskCommand);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
