using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;
using AutoMapper;

namespace WebApi.Application.Mapping
{
    public class ExpenditureMapping : Profile
    {
        public ExpenditureMapping()
        {
            CreateMap<Expenditure, ExpenditureDto>().ReverseMap();
        }
    }
}
