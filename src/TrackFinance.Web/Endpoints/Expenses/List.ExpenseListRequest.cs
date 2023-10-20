namespace TrackFinance.Web.Endpoints.Expenses;

public class ExpenseListRequest
{
  public const string Route = "/Expense/{UserId:int}/user";
  public static string BuildRoute(int userId) => Route.Replace("{UserId:int}", userId.ToString());
  public int UserId { get; set; }
}
