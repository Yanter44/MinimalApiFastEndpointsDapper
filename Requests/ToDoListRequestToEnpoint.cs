using Microsoft.AspNetCore.Mvc;

namespace MinimalApi_test____Dapper___PostgreSQL.Requests
{
    public class ToDoListRequestToEnpoint
    {
       
        public int PageNumber { get; set; }
      
        public int PageSize { get; set; }
    }
}
