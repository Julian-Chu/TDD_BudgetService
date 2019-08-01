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
      var budgets = _repo.GetAll();
      if (budgets.Count == 0)
      {
        return 0;
      }

      var budget = budgets.FirstOrDefault();
      if (end < budget.FirstDay || start > budget.LastDay)
      {
        return 0;
      }
      else
      {
        var endDate = end <= budget.LastDay ? end : budget.LastDay;
        var startDate = start > budget.FirstDay ? start : budget.FirstDay;
        return Days(startDate, endDate);
      }
      //return Days(start, end);
    }

    private int Days(DateTime start, DateTime end)
    {
      return end.Day - start.Day + 1;
    }
  }
}