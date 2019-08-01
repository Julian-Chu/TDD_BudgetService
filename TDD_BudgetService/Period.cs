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

    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public bool IsNoOverlapping(Budget budget)
    {
      return End < budget.FirstDay || Start > budget.LastDay;
    }
  }
}