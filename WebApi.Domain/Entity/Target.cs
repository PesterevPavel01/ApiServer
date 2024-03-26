using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Entity
{
    public class Target : IEntityId<short>, IAuditable
    {
        public short Id { get; set; }
        public double Value { get; set; }
        public Expenditure Expenditure { get; set; }
        public short? ExpenditureID { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
