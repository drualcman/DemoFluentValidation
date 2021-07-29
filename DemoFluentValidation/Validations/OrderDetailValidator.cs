using DemoFluentValidation.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFluentValidation.Validations
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(orderDetail => orderDetail.OrderId).GreaterThan(0);
            RuleFor(orderDetail => orderDetail.ProductId).GreaterThan(0);
            RuleFor(orderDetail => orderDetail.Quantity).GreaterThan(10);
            RuleFor(orderDetail => orderDetail.UnitPrice).GreaterThan(10);
            RuleFor(orderDetail => orderDetail.Discount).LessThanOrEqualTo(15);
        }
    }
}
