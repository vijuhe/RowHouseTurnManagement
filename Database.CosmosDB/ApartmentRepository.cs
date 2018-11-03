using System;
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
        private const string CollectionId = "Apartments";

        private readonly string _authenticationKey;
        private readonly Uri _endpointUri;

        public ApartmentRepository(Uri endpointUri, string authenticationKey)
        {
            _endpointUri = endpointUri;
            _authenticationKey = authenticationKey;
        }

        public async Task<Guid> AddApartment(string lastName, string rowId, int apartmentNumber, int postalCode)
        {
            var apartment = new Apartment
            {
                PostalCode = postalCode,
                LastName = lastName,
                RowId = rowId,
                ApartmentNumber = apartmentNumber
            };
            using (var documentClient = CreateDocumentClient())
            {
                await EnsureCollectionIsCreated(documentClient);
                ResourceResponse<Document> documentResponse = await documentClient
                    .CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), apartment);
                return Guid.Parse(documentResponse.Resource.Id);
            }
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
