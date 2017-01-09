using System;
using SQLite;

namespace FoodDatabase.Core.API.Models.Abstracts
{
    /// <summary>
    /// General class which all API models inherit.
    /// </summary>
    public class APIModel
    {
        [PrimaryKey]
        public int id { get; set; }
    }
}
