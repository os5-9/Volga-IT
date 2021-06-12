using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volga_IT.Models
{
    [Table("unique_word")]
    public class UniqueWord
    {
        [Key]
        [Column("id")]
        public int IdWord { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("word")]
        public string Word { get; set; }
        [Column("count")]
        public int? Count { get; set; }
    }
}
