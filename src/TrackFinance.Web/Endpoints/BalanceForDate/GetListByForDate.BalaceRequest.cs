using Microsoft.AspNetCore.Mvc;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.BalanceForDate;

public class BalaceForDateRequest
{
  public const string Route = "/BalanceForDate/{DateType}/{UserId}/{TransactionType}/{StartDate}/{EndDate}";
   
  public int UserId { get; set; }
  public DateType DateType { get; set; }
  public TransactionType TransactionType { get; set; }
   
  public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
   
  public DateTime EndDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));



}
