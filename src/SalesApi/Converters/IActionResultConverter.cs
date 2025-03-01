using Microsoft.AspNetCore.Mvc;
using Sales.Application.Shared;

namespace SalesApi.Converters
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(Result<T> response);
    }
}