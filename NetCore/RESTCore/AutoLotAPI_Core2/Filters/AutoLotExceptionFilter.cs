using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoLotAPI_Core2.Filters
{
    public class AutoLotExceptionFilter : IExceptionFilter
    {
        private readonly bool _isDevelopment;
        public AutoLotExceptionFilter(IWebHostEnvironment env)
        {
            _isDevelopment = env.IsDevelopment();
        }
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            string stackTrace = _isDevelopment ? context.Exception.StackTrace : string.Empty;
            IActionResult actionResult;
            string message = ex.Message;
            if (ex is DbUpdateConcurrencyException)
            {
                // Возвратить код 400
                if (!_isDevelopment)
                {
                    message = "There was an error updating the database. Another user has altered the record.";
                    // При обновлении базы данных возникла ошибка. Запись была изменена другим пользователем.
                }
                actionResult = new BadRequestObjectResult(new { Error = "Concurrency Issue.", Message = message, StackTrace = stackTrace });
                // Проблема параллелизма.
            }
            else
            {
                if (!_isDevelopment)
                {
                    message = "There was an unknown error. Please try again.";
                    // Возникла неизвестная ошибка. Повторите действие.
                }
                actionResult = new ObjectResult(new { Error = "General Error.", Message = message, StackTrace = stackTrace })
                // Ошибка общего характера.
                { StatusCode = 500 };
            }
            context.Result = actionResult;
        }
    }
}
