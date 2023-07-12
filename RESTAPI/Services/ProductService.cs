using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup", new BsonDocument
                {
                    {"from","CategoryCollection"},
                    {"localField","CategoryId"},
                    {"foreignField","_id"},
                    {"as","product_category" }
                }),
                new BsonDocument("$unwind","$product_category"),
                new BsonDocument("$project",new BsonDocument
                {
                    {"_id",1 },
                    {"CategoryId",1 },
                    {"ProductName",1 },
                    {"CategoryName","$product_category.CategoryName" }
                })
            };

            var results = await _productcollection.Aggregate<Product>(pipeline).ToListAsync();
            return results;

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
