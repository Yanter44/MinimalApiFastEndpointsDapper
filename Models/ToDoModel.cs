using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MinimalApi_test____Dapper___PostgreSQL.Models
{
    public class ToDoModel
    {
        [JsonIgnore]
        public int Id { get; set; }       
        public string WhatToDo { get; set; }
        
        public bool is_it_done { get; set; }

        public DateTime TimestampNow { get; set; }

        public DateTime DateTimeFieldMustDo { get; set; }
     
    }
}
