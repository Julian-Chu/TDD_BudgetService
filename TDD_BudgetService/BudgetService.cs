using System;

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
      return Days(start, end);
    }

    private int Days(DateTime start, DateTime end)
    {
      return end.Day - start.Day + 1;
    }
  }
}