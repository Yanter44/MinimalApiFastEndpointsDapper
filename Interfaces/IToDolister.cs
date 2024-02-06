using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Models;

namespace MinimalApi_test____Dapper___PostgreSQL.Interfaces
{
    public interface IToDolister
    {
        public Task<List<ToDoModel>> GetAllToDoList(CancellationToken cancellationToken);
        public Task<string> AddToDo(ToDoModelDtos todomodel);
        
    }
}
