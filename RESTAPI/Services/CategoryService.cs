using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RESTAPI.DataBase;
using RESTAPI.Models;

namespace RESTAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categorycollection;
        private readonly IOptions<DataBaseSettings> _dbsettings;

        public CategoryService(IOptions<DataBaseSettings> dbsettings)
        {
            _dbsettings = dbsettings;
            var mongoclient = new MongoClient(dbsettings.Value.ConnectionString);
            var mongodatabase = mongoclient.GetDatabase(dbsettings.Value.DatabaseName);
            _categorycollection = mongodatabase.GetCollection<Category>(dbsettings.Value.CategoriesCollectionName); 

        }


        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _categorycollection.Find(_ => true).ToListAsync();
            return  categories;
        }
        public async Task<Category> GetById(string id)
        {
            var categoryItem =  await _categorycollection.Find(x=>x.Id == id ).FirstOrDefaultAsync();
            return categoryItem;
        }
        public async Task CreateAsync(Category category)
        {
             await _categorycollection.InsertOneAsync(category);
        }
        public async Task Update(String id ,Category category)
        {
            await _categorycollection.ReplaceOneAsync(x => x.Id == id, category);
        }
        public async Task DeleteAsync(string id)
        {
            await _categorycollection.DeleteOneAsync(a=>a.Id == id);
        }

    }
}
