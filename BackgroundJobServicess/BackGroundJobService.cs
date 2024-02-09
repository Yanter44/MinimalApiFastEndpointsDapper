using Hangfire;
using MinimalApi_test____Dapper___PostgreSQL.DataBase;
using MinimalApi_test____Dapper___PostgreSQL.Models;
using Dapper;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Configuration;
namespace MinimalApi_test____Dapper___PostgreSQL.BackgroundJobServicess
{
    public class BackGroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IDbConnectionFactory _connectionFactory;   
        public BackGroundJobService(IBackgroundJobClient backgroundJobClient, IDbConnectionFactory connectionFactory)
        {
            _backgroundJobClient = backgroundJobClient;
            _connectionFactory = connectionFactory;
                
        }
        public void CheckScheduledTasks()
        {
            RecurringJob.AddOrUpdate("CheckScheduledTasks", () => CheckAndNotifyUsers(), Cron.MinuteInterval(1));
           
        }
        public async Task CheckAndNotifyUsers()
        {
            var currentTime = DateTime.Now;
            var formattedDateTime = currentTime.ToString("yyyy-MM-dd HH:mm");
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                var scheduledTasks = await connection.QueryAsync<ToDoModelDtos>($"SELECT * FROM ToDoList WHERE MustToCompleteTime = '{formattedDateTime}'");
                foreach (var task in scheduledTasks)
                {
                    Console.WriteLine($"Вы не выполнили задачу {task.WhatToDo} :(");
                }
                if (scheduledTasks.Count() == 0)
                {
                    Log.Information("Вcе задачи ждут вашего выполнения");
                }
            }
        }
    }
}
