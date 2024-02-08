using RestWithAspNet.Model.Base;
using RestWithAspNet.Repository.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNet.Model
{
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}
