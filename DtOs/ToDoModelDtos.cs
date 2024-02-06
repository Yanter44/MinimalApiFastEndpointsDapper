using System.Text.Json.Serialization;

namespace MinimalApi_test____Dapper___PostgreSQL.DtOs
{
    public class ToDoModelDtos
    {
       
        public string WhatToDo { get; set; }

        [JsonIgnore]   
        
        public bool is_it_done { get; set; }

        public string DateTimeNow { get; set; }

        public string DateTimeFieldMustDo { get; set; }
    }
}
