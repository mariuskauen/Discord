﻿using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Core.Data
{
    public class QueryRepository
    {
        private readonly IMongoDatabase database;

        public QueryRepository(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
        public async Task<E> GetSingle<T, E>(T first, E second, string query) where T : class, new() where E : class, new()
        {
            string[] queries = query.Split(':');

            var collection = database.GetCollection<T>(queries[0]);
            var filter = Builders<T>.Filter.Eq(queries[1], queries[2]);

            var ret = new E();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, E>());
            var mapper = new Mapper(config);
            try
            {
                return mapper.Map<E>(await collection.Find(filter).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                string buhu = ex.ToString();
            }
            return ret;
        }

        public async Task<List<E>> GetList<T, E>(T first, E second, string query) where T : class, new() where E : class, new()
        {
            string[] queries = query.Split(':');

            var collection = database.GetCollection<T>(queries[0]);
            var filter = Builders<T>.Filter.Eq(queries[1], queries[2]);

            List<E> ret = new List<E>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, E>());
            var mapper = new Mapper(config);
            try
            {
                return mapper.Map<List<E>>(await collection.Find(filter).ToListAsync());
            }
            catch (Exception ex)
            {
                string buhu = ex.ToString();
            }
            return ret;
        }
    }
}
