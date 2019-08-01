using System;

namespace Tests
{
  public class Budget
  {
    public decimal dailyAmount => Amount / (LastDay - FirstDay).Days;

    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public Period Period => new Period(FirstDay, LastDay);

    private DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

    private DateTime LastDay => FirstDay.AddMonths(1).AddDays(-1);
  }
}