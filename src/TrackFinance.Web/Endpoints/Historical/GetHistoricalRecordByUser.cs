﻿using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUser : EndpointBaseAsync
  .WithRequest<GetHistoricalRecordByUserRequest>
  .WithActionResult<GetHistoricalRecordByUserResponse>
{
  private readonly IRepository<Transaction> _repository;

  public GetHistoricalRecordByUser(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(GetHistoricalRecordByUserRequest.Route)]
  [SwaggerOperation(
      Summary = "Get Historical Records by user",
      Description = "Get Historical Records by userId",
      OperationId = "HistoricalRecords.GetHistoricalRecordByUser",
      Tags = new[] { "HistoricalRecordsEndpoints" })
  ]
  public override async Task<ActionResult<GetHistoricalRecordByUserResponse>> HandleAsync([FromRoute] GetHistoricalRecordByUserRequest request, CancellationToken cancellationToken = default) =>
    Ok(new GetHistoricalRecordByUserResponse
    {
      HistoricalRecord = (await _repository.ListAsync(cancellationToken))
         .Where(expense => expense.UserId == request.UserId)
         .Where(date => Convert.ToDateTime(date.ExpenseDate.ToString("d")) >= request.StartDate && Convert.ToDateTime(date.ExpenseDate.ToString("d")) <= request.EndDate)
         .Select(expense => new HistoricalRecord(
                                               description: expense.Description,
                                               transactionDescriptionType: expense.TransactionDescriptionType,
                                               amount: expense.Amount,
                                               expenseDate: expense.ExpenseDate,
                                               transactionType: expense.TransactionType))
         .OrderBy(d => d.expenseDate)
         .ToList()
    });
  //public override async Task<ActionResult<GetHistoricalRecordByUserResponse>> HandleAsync([FromRoute] GetHistoricalRecordByUserRequest request, CancellationToken cancellationToken = default)
  //{
  //  var response = new GetHistoricalRecordByUserResponse();
  //  response.HistoricalRecord = (await _repository.ListAsync(cancellationToken))
  //    .Where(expense => expense.UserId == request.UserId)
  //       .Where(date => Convert.ToDateTime(date.ExpenseDate.ToString("d")) >= request.StartDate && Convert.ToDateTime(date.ExpenseDate.ToString("d")) <= request.EndDate)
  //       .Select(expense => new HistoricalRecord(
  //                                             description: expense.Description,
  //                                             transactionDescriptionType: expense.TransactionDescriptionType,
  //                                             amount: expense.Amount,
  //                                             expenseDate: expense.ExpenseDate,
  //                                             transactionType: expense.TransactionType))
  //       .OrderBy(d => d.expenseDate)
  //       .ToList();

  //  return Ok(response);
  //}

}
