using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Entity
{
    public class User : IEntityId<short>, IAuditable
    {
        public User() { }
        public short Id { get; set ; }
        public string Name { get; set ; }

        public List<Expenditure> Expenditures { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime UpdatedAt { get; set; }
    }
}
