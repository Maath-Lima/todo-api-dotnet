using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOModels
{
    public abstract class BaseDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}

