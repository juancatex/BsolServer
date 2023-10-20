using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.BalanceForDate;

public class GetListByForDate : EndpointBaseAsync
    .WithRequest<BalaceForDateRequest>
    .WithActionResult<TransactionForDateRecord>
{
  private readonly ITransactionFinanceService _transactionService;

  public GetListByForDate(ITransactionFinanceService transactionService)
  {
    _transactionService = transactionService;
  }

  [HttpGet(BalaceForDateRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "Balance for Date",
    Description = "group",
    OperationId = "Balance.List",
    Tags = new[] { "BalanceForDateEndpoints" })
]
  public override async Task<ActionResult<TransactionForDateRecord>> HandleAsync([FromRoute] BalaceForDateRequest request, CancellationToken cancellationToken = default)
  {
    var transactions = await _transactionService.GetTransactionItemsByAsyncForDate(request.StartDate, request.EndDate, request.DateType, request.UserId, request.TransactionType, cancellationToken);

    if (transactions.Status != ResultStatus.Ok) return BadRequest(transactions.ValidationErrors);

    return Ok(new BalanceForDateListResponse
    {
      ExpensesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Expense),
      IncomesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Income)
    });
  }

  private static List<TransactionForDateRecord>? GetTransactionsRecords(List<TransactionDataDto> transactions, TransactionType transactionType)
  {
    return new List<TransactionForDateRecord>(transactions.Where(x => x.TransactionType == transactionType)
                       .Select(item => new TransactionForDateRecord(
                        item.Date,
                        item.DayOfWeek,
                        item.Day,
                        item.TotalAmount,
                        item.TransactionDescriptionType,
                        item.Week,
                        item.Year,
                        item.Month
                       )));
  }
}
