﻿using Microsoft.AspNetCore.Mvc;

namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUserRequest
{
  public const string Route = "/HistoricalRecords/{UserId:int}/user";
     public static string BuildRoute(int UserId, string? startDate, string? endDate) =>    Route.Replace("{UserId:int}", UserId.ToString()) +
    $"?startDate={startDate ?? DateTime.Now.ToString("d")}&endDate={endDate ?? DateTime.Now.ToString("d")}";
  public int UserId { get; set; }

  [FromQuery(Name = "startDate")]
  public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
  [FromQuery(Name = "endDate")]
  public DateTime EndDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
}
