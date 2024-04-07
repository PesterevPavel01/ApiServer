using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Dto.Target
{
    public record TargetDto(long? Id, string Expenditure,double Value,short Month,short Year);
}
