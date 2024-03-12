using System.Reflection.Metadata;
using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Entity
{
    public class Document : IEntityId<long>,IAuditable
    {
        public Document() { }
        public long Id { get; set ; }
        public double Value { get; set; }
        public string Comment { get; set; }
        public Expenditure Expenditure { get; set; }
        public short? ExpenditureID { get; set; }
        public Organization Organization { get; set; }
        public short? OrganizationID { get; set; }
        public Department Department { get; set; }
        public short? DepartmentID { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set ; }
    }

}
