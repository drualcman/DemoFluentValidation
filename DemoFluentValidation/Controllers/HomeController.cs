using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoFluentValidation.Entities;
using DemoFluentValidation.Validations;
using FluentValidation;
using FluentValidation.Results;
using System.Text;

namespace DemoFluentValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] Order order) 
        {
            ActionResult result = Ok();

            OrderValidator validator = new OrderValidator();
            ValidationResult validationResult = validator.Validate(order);
            
            if (validationResult.IsValid)
            {
                // guardar orden
            }
            else
            {
                //devolviendo Problem devuelve un 500 Internal Server Error (defecto puedes enviar cual quieres) con la el mensaje dentro de una propiedad detail :D
                result = Problem(validationResult.ToString(), statusCode: 401);
            }

            return result;
        }

        [HttpPost("create-exception")]
        public IActionResult CreateOrderExceptions([FromBody] Order order)
        {
            ActionResult result = Ok();

            OrderValidator validator = new OrderValidator();

            try
            {
                validator.ValidateAndThrow(order);
                //guardar la order
            }
            catch (ValidationException ex)
            {
                StringBuilder builder = new StringBuilder();
                foreach (ValidationFailure error in ex.Errors)
                {
                    builder.AppendFormat("La propiedad {0} Fallo la validacion en {1}{2}", error.PropertyName, error.ErrorMessage, Environment.NewLine);
                }
                //devolviendo ValidationProblem devuelve un 400 (por defecto, pero puedes personalizar) con la el mensaje dentro de una propiedad detail :D                
                ValidationProblemDetails validationProblem = new ValidationProblemDetails();
                validationProblem.Detail = builder.ToString();
                result = ValidationProblem(builder.ToString());
            }

            return result;
        }
    }
}
