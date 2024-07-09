using AutoMapper;
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
using System.Text.Json.Serialization;
using TaskManagement.Core.Application.Interfaces.Services;

namespace TaskManagement.Core.Application.Features.Task.Commands
{
    public class UpdateTaskCommand : IRequest<Response<int>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdTaskStatus { get; set; }

    }
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<int>>
    {
        private readonly IAccountService _accountService;
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IAccountService accountService, ITasksRepository tasksRepository, IMapper mapper)
        {
            _accountService = accountService;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
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
                    throw new ApiException("No puedes editar esta tarea,porque no es de tu propiedad.", (int)HttpStatusCode.Forbidden);
                }
                if (string.IsNullOrEmpty(request.Name) || request.Name == "string")
                {
                    request.Name = task.Name;
                }

                var entity = _mapper.Map<Tasks>(request);
                entity.IsDeleted = false;
                entity.IdUser = idUser;
                entity.ModifiedDate = DateTime.UtcNow;
                await _tasksRepository.UpdateAsync(entity, request.Id);
                return new Response<int>(1);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, ex);
            }
        }


    }
}
