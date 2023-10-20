using FluentValidation;
using TrackFinance.Web.Endpoints.Balance;

namespace TrackFinance.Web.Endpoints.Expense;

public class BalanceForDateValidatitor : AbstractValidator<BalaceRequest>
{
  public BalanceForDateValidatitor() 
  { 
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
