using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Dto.Order
{
    public record ExpenseReportDto(string Expenditure,double Value, string Responsible,double? Target);
}
