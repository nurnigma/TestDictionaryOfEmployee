using System.ComponentModel.DataAnnotations;

namespace DictionaryOfEmployee.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }
    }
}
