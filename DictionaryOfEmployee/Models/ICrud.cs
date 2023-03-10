using Microsoft.AspNetCore.Mvc;

namespace DictionaryOfEmployee.Models
{
    public interface ICrud<T>
    {
        Task<IActionResult> GetById(int id);   // возращает лист
        Task <IActionResult> Add(T t);
        Task <IActionResult> Remove(int id);
        Task <IActionResult> Edit(T t);
    }
}
