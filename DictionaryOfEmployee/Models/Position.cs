using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryOfEmployee.Models
{
    public class Position
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [Remote(action: "CheckName", controller: "Position", ErrorMessage = "Эта должность уже используется")]
        public string Name { get; set; }
    }
}
