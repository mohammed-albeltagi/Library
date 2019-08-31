using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BaseEntity
    {
        [Key, Required]
        public int Id { get; set; }
    }
}
