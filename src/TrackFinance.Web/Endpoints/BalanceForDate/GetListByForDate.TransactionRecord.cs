using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.BalanceForDate;

public record TransactionForDateRecord( DateTime Date, DayOfWeek? DayOfWeek, int Day, decimal TotalAmount, TransactionDescriptionType TransactionDescriptionType, int Week, int Year, int Month);
