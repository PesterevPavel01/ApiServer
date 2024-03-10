using AutoMapper;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;

namespace WebApi.Application.Mapping
{
    public class DocumentMapping:Profile
    {
        public DocumentMapping()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();
        }
    }
}
