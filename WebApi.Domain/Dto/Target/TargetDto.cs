using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Dto.Target
{
    public record TargetDto(short? Id, string Expenditure,double Value,DateTime Date);
}
