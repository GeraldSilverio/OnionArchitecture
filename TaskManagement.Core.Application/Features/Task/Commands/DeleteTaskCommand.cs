using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Application.Interfaces;
using TaskManagement.Core.Application.Wrappers;
using TaskManagement.Core.Application.Exceptions;
using System.Net;
using TaskManagement.Core.Domain.Entities;
using AutoMapper;
using TaskManagement.Core.Application.Interfaces.Services;

namespace TaskManagement.Core.Application.Features.Task.Commands
{
    public class DeleteTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Response<int>>
    {
        private readonly IAccountService _accountService;
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public DeleteTaskCommandHandler(IAccountService accountService, ITasksRepository tasksRepository, IMapper mapper)
        {
            _accountService = accountService;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string idUser = _accountService.GetIdUser();
                var task = await _tasksRepository.GetByIdAsync(request.Id);
                if (task == null)
                {
                    throw new ApiException("Tasks not found", (int)HttpStatusCode.NoContent);
                }
                if (task.IdUser != idUser)
                {
                    throw new ApiException("No puedes eliminar esta tarea,porque no es de tu propiedad.", (int)HttpStatusCode.Forbidden);
                }
                var entity = _mapper.Map<Tasks>(task);
                await _tasksRepository.DeleteAsync(entity);
                return new Response<int>(1);

            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
