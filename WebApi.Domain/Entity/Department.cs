﻿using WebApi.Domain.Interfaces;

namespace WebApi.Domain.Entity
{
    public class Department:IEntityId<short>,IAuditable
    {
        public Department() { }
        public short Id { get; set; }
        public string Name { get; set; }
        public List<Document> Documents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
