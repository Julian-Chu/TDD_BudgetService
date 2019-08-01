using System;

namespace Tests
{
  public class Budget
  {
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

    public DateTime LastDay => FirstDay.AddMonths(1).AddDays(-1);
  }
}