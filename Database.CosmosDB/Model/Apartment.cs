using System;

namespace RowHouseTurnManagement.DB.Cosmos.Model
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
