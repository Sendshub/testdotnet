using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDotNet.Models
{
    public class DataItem
    {
        /// <summary>
        /// Порядковый номер
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Код
        /// </summary>
        [Required]
        public int Code { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        [Required]
        public string? Value { get; set; }
    }
}