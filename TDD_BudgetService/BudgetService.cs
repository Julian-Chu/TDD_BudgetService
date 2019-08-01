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
      if (period.IsNoOverlapping(budget))
      {
        return 0;
      }
      else
      {
        var endDate = period.End <= budget.LastDay ? period.End : budget.LastDay;
        var startDate = period.Start > budget.FirstDay ? period.Start : budget.FirstDay;
        return Days(startDate, endDate);
      }
    }

    private int Days(DateTime start, DateTime end)
    {
      return end.Day - start.Day + 1;
    }
  }
}