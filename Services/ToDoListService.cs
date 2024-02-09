using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MinimalApi_test____Dapper___PostgreSQL.DataBase;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using MinimalApi_test____Dapper___PostgreSQL.Models;

namespace MinimalApi_test____Dapper___PostgreSQL.Services
{
    public class ToDoListService : IToDolister
    {
        public const string TableName = "ToDoList";

        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "ToDoListik";
        public ToDoListService(IDbConnectionFactory connectionFactory, IMemoryCache cache)
        {
            _connectionFactory = connectionFactory;
            _cache = cache;
        }

        public async Task<string> AddToDo(ToDoModelDtos todomodelDto)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync($@"INSERT INTO {TableName} 
                                                (WhatToDo,is_it_done,DataTimeNow,MustToCompleteTime) 
                                                VALUES ('{todomodelDto.WhatToDo}', 'false', '{todomodelDto.DateTimeNow}','{todomodelDto.DateTimeFieldMustDo}')");
                _cache.Remove(CacheKey);
                return "You Added new To Do";
            }
        }

        public async Task<List<ToDoModel>> GetAllToDoList(CancellationToken ct)
        {         
            if (!_cache.TryGetValue(CacheKey, out List<ToDoModel> toDoList))
            {
                using (var connection = await _connectionFactory.CreateConnectionAsync())
                {

                    var result = await connection.QueryAsync<ToDoModel>($"SELECT *, CAST(DataTimeNow AS datetime) AS DateTimeNow," +
                                                                        $" CAST(MustToCompleteTime AS datetime) AS DateTimeFieldMustDo" +
                                                                        $" FROM {TableName}", ct);

                    toDoList = result.ToList();
                }
              _cache.Set(CacheKey, toDoList, TimeSpan.FromMinutes(10));
            }
           return toDoList;
        }
           
    }
}

