using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateExpenseRequest>
    .WithActionResult<UpdateExpensesResponse>
{
  private readonly IRepository<Transaction> _repository;

  public Update(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateExpenseRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
     Summary = "Updates a Expenses (Update)",
     Description = "Updates a Expenses",
     OperationId = "Income.Expense",
     Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<UpdateExpensesResponse>> HandleAsync(UpdateExpenseRequest request, CancellationToken cancellationToken = default)
  {
    var existingIncomes = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingIncomes == null)
    {
      return NotFound();
    }

    existingIncomes.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Income);

    await _repository.UpdateAsync(existingIncomes, cancellationToken);

    var response = new UpdateExpensesResponse(
        expenseRecord: new ExpenseRecord(
          existingIncomes.Id,
          existingIncomes.Description,
          existingIncomes.Amount,
          existingIncomes.TransactionDescriptionType,
          existingIncomes.ExpenseDate,
          existingIncomes.UserId,
          existingIncomes.TransactionType));

    return Ok(response);
  }
}
