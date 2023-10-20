using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expense;

public class UpdateExpensesValidator : AbstractValidator<UpdateIncomeRequest>
{
  public UpdateExpensesValidator() 
  { 
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
  }
}
