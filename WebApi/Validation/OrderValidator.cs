using CompanyName.Application.WebApi.OrdersApi.Models.Orders;
using FluentValidation;

namespace CompanyName.Application.WebApi.OrdersApi.Validation
{
    public class OrderValidator : AbstractValidator<OrderDtoBase>
    {
        public OrderValidator() 
        {
            RuleFor(request => request.Number)
                .NotEmpty().WithMessage("Number is mandatory")
                .MaximumLength(100).WithMessage("Number length must be less then");
        }
    }
}
