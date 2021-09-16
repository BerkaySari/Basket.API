using Data.Models.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{

    public abstract class RedisDataService<TContext, TModel> : IDataService where TContext : class
    {
        public TContext Context { get; set; }

        protected RedisDataService(TContext context)
        {
            Context = context;
        }
    }

    public class RedisDataService<TModel> : RedisDataService<IDistributedCache, TModel>, IDataService<TModel> where TModel : class, IBaseEntity
    {
        public RedisDataService(IDistributedCache context) : base(context)
        {
        }

        #region Async Methods

        public virtual async Task DeleteItemAsync(string key)
        {
            await Context.RemoveAsync(key);
        }

        public virtual async Task<TModel> GetItemAsync(string key)
        {
            var item = await Context.GetStringAsync(key);
            if (item == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TModel>(item);
        }

        public virtual async Task<List<TModel>> GetItemListAsync(string key)
        {
            var item = await Context.GetStringAsync(key);
            if (item == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<TModel>>(item);
        }

        public virtual async Task<TModel> UpdateItemAsync(string key, TModel model)
        {
            var entity = model as IBaseEntity;
            if (entity == null)
            {
                return model;
            }

            entity.LastModifiedDate = DateTimeOffset.UtcNow;

            var item = JsonConvert.SerializeObject(entity);
            await Context.SetStringAsync(key, item);

            return model;
        }

        public virtual async Task<TModel> CreateItemAsync(string key, TModel model)
        {
            var item = JsonConvert.SerializeObject(model);
            await Context.SetStringAsync(key, item);
            return model;
        }

        public virtual async Task<List<TModel>> CreateOrUpdateItemListAsync(string key, List<TModel> model)
        {
            var item = JsonConvert.SerializeObject(model);
            await Context.SetStringAsync(key, item);
            return model;
        }
        #endregion Async Methods

        #region Sync Methods
        public virtual void DeleteItem(string key)
        {
            Context.Remove(key);
        }

        public virtual TModel GetItem(string key)
        {
            var item = Context.GetString(key);
            if (item == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TModel>(item);
        }

        public virtual List<TModel> GetItemList(string key)
        {
            var item = Context.GetString(key);
            if (item == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<TModel>>(item);
        }

        public virtual TModel UpdateItem(string key, TModel model)
        {
            var entity = model as IBaseEntity;
            if (entity == null)
            {
                return model;
            }

            entity.LastModifiedDate = DateTimeOffset.UtcNow;

            var item = JsonConvert.SerializeObject(entity);
            Context.SetString(key, item);

            return model;
        }

        public virtual TModel CreateItem(string key, TModel model)
        {
            var item = JsonConvert.SerializeObject(model);
            Context.SetString(key, item);
            return model;
        }

        public virtual List<TModel> CreateOrUpdateItemList(string key, List<TModel> model)
        {
            var item = JsonConvert.SerializeObject(model);
            Context.SetString(key, item);
            return model;
        }
        #endregion Sync Methods
    }
}
