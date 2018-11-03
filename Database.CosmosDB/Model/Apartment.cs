using System;

namespace RowHouseTurnManagement.DB.Cosmos.Model
{
    public class Apartment
    {
        public string LastName { get; set; }
        public string RowId { get; set; }
        public int ApartmentNumber { get; set; }
        public int PostalCode { get; set; }
    }
}
