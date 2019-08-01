using System;
using System.Linq;

namespace Tests
{
  public class BudgetService
  {
    private readonly IBudgetRepository _repo;

    public BudgetService(IBudgetRepository repo)
    {
      _repo = repo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
      var period = new Period(start, end);
      var budgets = _repo.GetAll();
      if (budgets.Count == 0)
      {
        return 0;
      }

      var budget = budgets.FirstOrDefault();
      var budgetPeriod = budget.Period;
      if (period.IsNoOverlapping(budgetPeriod))
      {
        return 0;
      }
      else
      {
        return OverlappingDays(period, budgetPeriod);
      }
    }

    private decimal OverlappingDays(Period period, Period budgetPeriod)
    {
      var endDate = period.End <= budgetPeriod.End ? period.End : budgetPeriod.End;
      var startDate = period.Start > budgetPeriod.Start ? period.Start : budgetPeriod.Start;
      return Days(startDate, endDate);
    }

    private int Days(DateTime start, DateTime end)
    {
      return end.Day - start.Day + 1;
    }
  }
}