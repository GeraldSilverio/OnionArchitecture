using MediatR;
using System.Net;
using TaskManagement.Core.Application.Dtos.TaskDtos;
using TaskManagement.Core.Application.Exceptions;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Application.Interfaces.Services;
using TaskManagement.Core.Application.Wrappers;

namespace TaskManagement.Core.Application.Features.Task.Queries
{
    public class GetAllTasksQuery : IRequest<Response<List<TaskDto>>>
    {
    }

    public class GetAllATasksQueryHandler : IRequestHandler<GetAllTasksQuery, Response<List<TaskDto>>>
    {
        private readonly IAccountService _accountService;
        private readonly ITasksRepository _tasksRepository;

        public GetAllATasksQueryHandler(IAccountService accountService, ITasksRepository tasksRepository)
        {
            _accountService = accountService;
            _tasksRepository = tasksRepository;
        }

        public async Task<Response<List<TaskDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string idUser = _accountService.GetIdUser();
                var tasks = await _tasksRepository.GetAllAsync();

                if (tasks.Count == 0)
                {
                    throw new ApiException("Tasks not found", (int)HttpStatusCode.NoContent);
                }
                var taskListDto = new List<TaskDto>();
                foreach (var task in tasks)
                {
                    var taskDto = new TaskDto()
                    {
                        IdUser = task.IdUser,
                        Id = task.Id,
                        Name = task.Name,
                        IdTaskStatus = task.IdTaskStatus,
                        Status = task.TaskStatus.Name
                    };
                    taskListDto.Add(taskDto);
                }
                return new Response<List<TaskDto>>(taskListDto);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}