using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using RowHouseTurnManagement.Application;
using RowHouseTurnManagement.DB.Cosmos.Model;

namespace RowHouseTurnManagement.DB.Cosmos
{
    public class ApartmentRepository : IApartmentRepository
    {
        private const string DatabaseId = "RowHouseTurnManagement";
        private const string CollectionId = "RowHouses";

        private readonly string _authenticationKey;
        private readonly Uri _endpointUri;

        public ApartmentRepository(Uri endpointUri, string authenticationKey)
        {
            _endpointUri = endpointUri;
            _authenticationKey = authenticationKey;
        }

        public async Task<Guid> AddApartment(int postalCode, string rowAddress, string lastName, int apartmentNumber)
        {
            Guid apartmentId = Guid.NewGuid();
            var apartment = new Apartment
            {
                Id = apartmentId,
                LastName = lastName,
                ApartmentNumber = apartmentNumber
            };
            using (var documentClient = CreateDocumentClient())
            {
                Document document = GetRowHouse(documentClient, postalCode, rowAddress);
                RowHouse rowHouse = RowHouse.LoadFrom(document);
                rowHouse.AddApartment(apartment);
                await documentClient.ReplaceDocumentAsync(rowHouse.Document);
            }
            return apartmentId;
        }

        public async Task AddRowHouse(int postalCode, string address)
        {
            var rowHouse = new RowHouse(postalCode, address);
            using (var documentClient = CreateDocumentClient())
            {
                await EnsureCollectionIsCreated(documentClient);
                await documentClient.CreateDocumentAsync(CreateRowHouseCollectionUri(), rowHouse);
            }
        }

        public async Task<bool> HasRowHouse(int postalCode, string address)
        {
            using (var documentClient = CreateDocumentClient())
            {
                await EnsureCollectionIsCreated(documentClient);
                return GetRowHouses(documentClient, postalCode, address).AsEnumerable().Any();
            }
        }

        private static string RemoveWhiteSpacesAndCapitalize(string address)
        {
            return address.Replace(" ", string.Empty).ToUpper();
        }

        private static Document GetRowHouse(DocumentClient documentClient, int postalCode, string rowAddress)
        {
            return GetRowHouses(documentClient, postalCode, rowAddress).AsEnumerable().Single();
        }

        private static IQueryable<dynamic> GetRowHouses(DocumentClient documentClient, int postalCode, string rowAddress)
        {
            string comparableAddress = RemoveWhiteSpacesAndCapitalize(rowAddress);
            string sqlQuery = $"select * from c where c.PostalCode = {postalCode} and c.AddressKey = '{comparableAddress}'";
            return documentClient.CreateDocumentQuery(CreateRowHouseCollectionUri(), sqlQuery);
        }

        private static Uri CreateRowHouseCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
        }

        private async Task<DocumentCollection> EnsureCollectionIsCreated(IDocumentClient documentClient)
        {
            await documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseId });
            ResourceResponse<DocumentCollection> collectionResponse = await documentClient
                .CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = CollectionId });
            return collectionResponse.Resource;
        }

        private DocumentClient CreateDocumentClient()
        {
            return new DocumentClient(_endpointUri, _authenticationKey);
        }
    }
}
