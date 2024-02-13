using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MinimalApi_test____Dapper___PostgreSQL.DataBase;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using MinimalApi_test____Dapper___PostgreSQL.Models;
using MinimalApi_test____Dapper___PostgreSQL.Requests;
using NUnit.Framework;

namespace MinimalApi_test____Dapper___PostgreSQL.Services
{
    public class ToDoListService : IToDolister
    {
        public const string TableName = "ToDoList";

        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ICacheRedisService _cacheRedis;
        private const string CacheKey = "ToDoListik";
        public ToDoListService(IDbConnectionFactory connectionFactory, ICacheRedisService cache)
        {
            _connectionFactory = connectionFactory;
            _cacheRedis = cache;
        }
        public async Task<string> AddToDo(ToDoModelDtos todomodelDto)
        {

            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await connection.ExecuteAsync($@"INSERT INTO {TableName} 
                                                (Id,WhatToDo,is_it_done,DataTimeNow,MustToCompleteTime) 
                                                VALUES ('{Guid.NewGuid()}','{todomodelDto.WhatToDo}', 'false', '{todomodelDto.DateTimeNow}','{todomodelDto.DateTimeFieldMustDo}')");
                var expirationtime = TimeSpan.FromMinutes(3);
                _cacheRedis.SetData(CacheKey, todomodelDto, expirationtime);
                return "You Added new To Do" ?? throw new Exception();
            }
        }

        public async Task<List<ToDoModel>> GetAllToDoList(ToDoListRequestToEnpoint requestPaging, CancellationToken ct)
        {
            var cachedRediData = _cacheRedis.GetData<List<ToDoModel>>(CacheKey);
            if (cachedRediData != null && cachedRediData.Count() > 0)
            {
                return cachedRediData;
            }
            else
            {
                using (var connection = await _connectionFactory.CreateConnectionAsync())
                {
                    int offset = (requestPaging.PageNumber - 1) * requestPaging.PageSize;
                    var result = await connection.QueryAsync<ToDoModel>($"SELECT *, CAST(DataTimeNow AS datetime) AS DateTimeNow," +
                                                                        $" CAST(MustToCompleteTime AS datetime) AS DateTimeFieldMustDo" +
                                                                        $" FROM {TableName} ORDER BY MustToCompleteTime OFFSET {offset} ROWS FETCH NEXT {requestPaging.PageSize} ROWS ONLY", ct);

                    var expirationtime = TimeSpan.FromMinutes(3);
                    _cacheRedis.SetData(CacheKey, result, expirationtime);
                    var toDoList = result.ToList();
                    return toDoList ?? throw new Exception();
                }
                
            }
        }

        public async Task<string> DeleteToDo(int IdToDo)
        {
            try
            {
                using (var connection = await _connectionFactory.CreateConnectionAsync())
                {
                    var exist = await connection.QueryAsync<ToDoModel>($"Select * From {TableName} Where Id = {IdToDo}");
                    if(exist != null)
                    {
                       var result = await connection.ExecuteAsync($"Remove From ToDoList where Id = {IdToDo}");
                        _cacheRedis.DeleteData(CacheKey, IdToDo);
                       return "You Deleted ToDo";
                    }                                              
                }
                return "ToDo with the specified Id does not exist.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }                       
        }
    }
        
}




