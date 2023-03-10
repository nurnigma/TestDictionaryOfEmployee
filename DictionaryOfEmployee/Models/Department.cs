using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryOfEmployee.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Поле пустое")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите организацию")]
        [ForeignKey("OrganizationId")]
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }

    }
}
