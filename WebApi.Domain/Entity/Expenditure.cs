using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Entity
{
    public class Expenditure : IEntityId<short> , IAuditable
    {
        public Expenditure() { }
        public short Id { get ; set ; }
        public string Name { get ; set ; }
        public User User { get ; set ; }
        public short? UserID { get; set; }
        public List<Document> Documents { get; set; }
        public List<Target> Targets { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
