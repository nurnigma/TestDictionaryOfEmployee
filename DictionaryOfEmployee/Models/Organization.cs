using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryOfEmployee.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [Remote(action: "CheckName", controller: "Organization", ErrorMessage = "Это название уже используется")]
        public string Name { get; set; }

       // public List<Department> Departments { get; set; }
    }
}
