namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesResponse
{
  public ExpenseRecord _expenseRecord;

  public UpdateExpensesResponse(ExpenseRecord expenseRecord)
  {
    _expenseRecord = expenseRecord;
  }
}
