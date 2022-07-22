using ContactosAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace ContactosAPI.Servicio
{
    public class ContactsService
    {

        private readonly IMongoCollection<Contacto> _contactosCollection;

        public ContactsService(
            IOptions<ContactsDatabaseSettings> ContactoStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ContactoStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ContactoStoreDatabaseSettings.Value.DatabaseName);

            _contactosCollection = mongoDatabase.GetCollection<Contacto>(
                ContactoStoreDatabaseSettings.Value.ContactsCollectionName);
        }

        public async Task<List<Contacto>> GetAsync() =>
            await _contactosCollection.Find(_ => true).SortBy(c => c.name).ToListAsync();

        public async Task<List<Contacto>> GetNameAsync(string name)
        {
            return await _contactosCollection.Find(Builders<Contacto>.Filter.Where(x=>x.name.Contains(name))).ToListAsync();
        }
            

        public async Task<Contacto?> GetAsync(string id) =>
            await _contactosCollection.Find(x => x.id.Equals((new ObjectId(id)))).FirstOrDefaultAsync();

        public async Task CreateAsync(Contacto newContacto) =>
            await _contactosCollection.InsertOneAsync(newContacto);

        public async Task UpdateAsync(string id, Contacto updatedContacto) =>
            await _contactosCollection.ReplaceOneAsync(x => x.id.Equals((new ObjectId(id))), updatedContacto);

        public async Task RemoveAsync(string id) =>
            await _contactosCollection.DeleteOneAsync(x => x.id.Equals((new ObjectId(id))));
    }
}
