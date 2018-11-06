using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace RowHouseTurnManagement.DB.Cosmos.Model
{
    public class RowHouse
    {
        public RowHouse(int postalCode, string address)
        {
            PostalCode = postalCode;
            Address = address;
        }

        public int PostalCode { get; set; }
        public string Address { get; set; }
        public string AddressKey => Address.Replace(" ", string.Empty).ToUpper();
        public List<Apartment> Apartments { get; set; }
        internal Document Document { get; private set; }

        public void AddApartment(Apartment apartment)
        {
            if (Apartments == null)
            {
                Apartments = new List<Apartment>();
            }
            Apartments.Add(apartment);
            Document.SetPropertyValue("Apartments", Apartments);
        }

        public static RowHouse LoadFrom(Document document)
        {
            RowHouse instance = (dynamic) document;
            instance.Document = document;
            return instance;
        }
    }
}
