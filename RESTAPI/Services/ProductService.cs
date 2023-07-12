using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RESTAPI.DataBase;
using RESTAPI.Models;

namespace RESTAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productcollection;
        private readonly IOptions<DataBaseSettings> _dbsettings;

        public ProductService(IOptions<DataBaseSettings> dbsettings)
        {
            _dbsettings = dbsettings;
            var mongoclient = new MongoClient(dbsettings.Value.ConnectionString);
            var mongodatabase = mongoclient.GetDatabase(dbsettings.Value.DatabaseName);
            _productcollection = mongodatabase.GetCollection<Product>(dbsettings.Value.ProductsCollectionName); 

        }


        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _productcollection.Find(_ => true).ToListAsync();
            return  products;
        }
        public async Task<Product> GetById(string id)
        {
            var productItem =  await _productcollection.Find(x=>x.Id == id ).FirstOrDefaultAsync();
            return productItem;
        }
        public async Task CreateAsync(Product product)
        {
             await _productcollection.InsertOneAsync(product);
        }
        public async Task Update(String id ,Product product)
        {
            await _productcollection.ReplaceOneAsync(x => x.Id == id, product);
        }
        public async Task DeleteAsync(string id)
        {
            await _productcollection.DeleteOneAsync(a=>a.Id == id);
        }

    }
}
