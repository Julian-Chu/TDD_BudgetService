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
      if (end < budget.FirstDay)
      {
        return 0;
      }
      return Days(start, end);
    }

    private int Days(DateTime start, DateTime end)
    {
      return end.Day - start.Day + 1;
    }
  }
}