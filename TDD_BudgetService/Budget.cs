using System;

namespace Tests
{
  public class Budget
  {
    public decimal DailyAmount => Amount / ((LastDay - FirstDay).Days + 1);

    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public Period Period => new Period(FirstDay, LastDay);

    private DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

    private DateTime LastDay => FirstDay.AddMonths(1).AddDays(-1);
  }
}