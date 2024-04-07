using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Target;
using WebApi.Domain.Entity;

namespace WebApi.Application.Mapping
{
    internal class TargetMapping : Profile
    {
            public TargetMapping()
            {
                CreateMap<Target, TargetDto>().ReverseMap();
            }
    }
}
