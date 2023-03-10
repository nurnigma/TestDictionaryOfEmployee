using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryOfEmployee.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Выберите подразделение")]
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Required(ErrorMessage = "Выберите должность")]
        [ForeignKey("PositionId")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }

        [Required(ErrorMessage = "Выберите пользователя")]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [EmailAddress(ErrorMessage ="Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "Employee", ErrorMessage = "Этот email уже используется")]

        public string Email { get; set; }

        

    }
}
