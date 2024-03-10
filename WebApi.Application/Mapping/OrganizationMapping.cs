using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Entity;

namespace WebApi.Application.Mapping
{
    public class OrganizationMapping: Profile
    {
        public OrganizationMapping()
        {
            CreateMap<Organization, OrganizationDto>().ReverseMap();
        }
    }
}
