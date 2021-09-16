using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDataService : IService
    {
    }

    public interface IDataService<TModel> : IDataService
    {
        #region Async Methods
        Task DeleteItemAsync(string key);
        Task<TModel> GetItemAsync(string key);
        Task<TModel> UpdateItemAsync(string key, TModel model);
        Task<TModel> CreateItemAsync(string key, TModel model);
        Task<List<TModel>> GetItemListAsync(string key);
        Task<List<TModel>> CreateOrUpdateItemListAsync(string key, List<TModel> model);
        #endregion Async Methods

        #region Sync Methods
        void DeleteItem(string key);
        TModel GetItem(string key);
        TModel UpdateItem(string key, TModel model);
        TModel CreateItem(string key, TModel model);
        List<TModel> GetItemList(string key);
        List<TModel> CreateOrUpdateItemList(string key, List<TModel> model);
        #endregion Sync Methods
    }

    public interface IService
    {
    }
}
