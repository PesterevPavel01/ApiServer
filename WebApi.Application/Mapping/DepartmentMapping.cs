using AutoMapper;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;

namespace WebApi.Application.Mapping
{
    public class DepartmentMapping:Profile
    {
        public DepartmentMapping() 
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
        }
    }
}
