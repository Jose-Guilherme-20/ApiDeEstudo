using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud_Api.Model
{
    [Table("book")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("launchDate")]
        public DateTime LaunchDate { get; set; }
    }
}