using MediatR;
using System.Net;
using TaskManagement.Core.Application.Enums;
using TaskManagement.Core.Application.Exceptions;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Application.Interfaces.Services;
using TaskManagement.Core.Application.Wrappers;
using TaskManagement.Core.Domain.Entities;

namespace TaskManagement.Core.Application.Features.Task.Commands
{
    public class CreateTaskCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }

    }
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Response<int>>
    {
        private readonly IAccountService _accountService;
        private readonly ITasksRepository _tasksRepository;

        public CreateTaskCommandHandler(IAccountService accountService, ITasksRepository tasksRepository)
        {
            _accountService = accountService;
            _tasksRepository = tasksRepository;
        }

        public async Task<Response<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string idUser = _accountService.GetIdUser();
                var entity = new Tasks(request.Name, idUser, (int)TasksStatuS.New, idUser);
                var tasks = await _tasksRepository.CreateAsync(entity);

                if (tasks == null)
                {
                    throw new ApiException("Tasks cannot be created", (int)HttpStatusCode.BadRequest);
                }

                return new Response<int>(tasks.Id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
