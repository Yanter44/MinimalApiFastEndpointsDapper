using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Models;
using MinimalApi_test____Dapper___PostgreSQL.Requests;

namespace MinimalApi_test____Dapper___PostgreSQL.Interfaces
{
    public interface IToDolister
    {
        public Task<List<ToDoModel>> GetAllToDoList(ToDoListRequestToEnpoint requestPaging, CancellationToken cancellationToken);
        public Task<string> AddToDo(ToDoModelDtos todomodel);
        public Task<string> DeleteToDo(int IdToDo);
        
    }
}
