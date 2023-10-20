using FluentValidation; 

namespace TrackFinance.Web.Endpoints.Expenses;

public class GetExpensesByIdValidator : AbstractValidator<GetExpenseByIdRequest>
{
  public GetExpensesByIdValidator()
  {
    RuleFor(expense => expense.ExpenseId).GreaterThan(0);
  }
}
