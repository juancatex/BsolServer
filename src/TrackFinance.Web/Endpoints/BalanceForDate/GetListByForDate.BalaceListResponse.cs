namespace TrackFinance.Web.Endpoints.BalanceForDate;

public class BalanceForDateListResponse
{
  public List<TransactionForDateRecord>? ExpensesTransaction { get; set; }
  public List<TransactionForDateRecord>? IncomesTransaction { get; set; }
}
