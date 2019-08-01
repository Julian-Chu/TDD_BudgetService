using System;

namespace Tests
{
  public class Period
  {
    public Period(DateTime start, DateTime end)
    {
      Start = start;
      End = end;
    }

    private DateTime Start { get; set; }
    private DateTime End { get; set; }

    public bool IsNoOverlapping(Period anotherPeriod)
    {
      return End < anotherPeriod.Start | Start > anotherPeriod.End;
    }

    public decimal OverlappingDays(Period budgetPeriod)
    {
      var endDate = End <= budgetPeriod.End ? End : budgetPeriod.End;
      var startDate = Start > budgetPeriod.Start ? Start : budgetPeriod.Start;
      return endDate.Day - startDate.Day + 1;
    }
  }
}