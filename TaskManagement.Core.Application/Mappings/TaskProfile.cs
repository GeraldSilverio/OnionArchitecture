using AutoMapper;
using TaskManagement.Core.Application.Dtos.TaskDtos;
using TaskManagement.Core.Application.Features.Task.Commands;
using TaskManagement.Core.Domain.Entities;

namespace TaskManagement.Core.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDto, Tasks>()
                .ForMember(x=> x.CreatedBy, opt=> opt.Ignore())
                .ForMember(x=> x.CreatedDate, opt=> opt.Ignore())
                .ForMember(x=> x.TaskStatus, opt=> opt.Ignore())
                .ForMember(x=> x.IsDeleted, opt=> opt.Ignore())
                .ForMember(x=> x.ModifiedBy, opt=> opt.Ignore())
                .ForMember(x=> x.ModifiedDate, opt=> opt.Ignore())
                .ReverseMap(); 
            
            CreateMap<UpdateTaskCommand, Tasks>()
                .ForMember(x=> x.CreatedBy, opt=> opt.Ignore())
                .ForMember(x=> x.CreatedDate, opt=> opt.Ignore())
                .ForMember(x=> x.TaskStatus, opt=> opt.Ignore())
                .ForMember(x=> x.IsDeleted, opt=> opt.Ignore())
                .ForMember(x=> x.ModifiedBy, opt=> opt.Ignore())
                .ForMember(x=> x.ModifiedDate, opt=> opt.Ignore())
                .ReverseMap();
                
        }
    }
}
