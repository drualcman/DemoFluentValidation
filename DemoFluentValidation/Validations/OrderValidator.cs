using DemoFluentValidation.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFluentValidation.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.OrderId).GreaterThan(0);
            RuleFor(order => order.CustomerId).GreaterThan(0);
            RuleFor(order => order.ShipAddress).NotNull();
            RuleFor(order => order.ShipCity).NotNull().NotEmpty();
            RuleFor(order => order.ShipName).NotNull().WithMessage("Tu nombre no puede no puede ser nulo").NotEqual("food").WithMessage("Tu nombre no puede ser comida");
            RuleFor(order => order.ShipPostalCode).MinimumLength(5);
            //RuleFor(order => order.ShipPostalCode).CreditCard();
            //RuleFor(order => order.ShipName).Matches("^(X|Y)?$").When(order => order.ShipName != null);
            RuleFor(order => order.OrderDetails).NotNull();
            RuleForEach(o => o.OrderDetails).SetValidator(new OrderDetailValidator());
        }
    }
}
