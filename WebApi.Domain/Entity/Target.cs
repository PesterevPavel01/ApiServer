using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Entity
{
    public class Target : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }
        public double Value { get; set; }
        public Expenditure Expenditure { get; set; }
        public short? ExpenditureID { get; set; }
        public short Month { get; set; }
        public short Year { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
