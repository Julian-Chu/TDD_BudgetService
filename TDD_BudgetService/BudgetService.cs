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
      if (start > end)
      {
        return 0;
      }
      var period = new Period(start, end);

      return _repo.GetAll().Where(b => !period.IsNoOverlapping(b.Period))
         .Sum(b => period.OverlappingDays(b.Period) * b.DailyAmount);
    }
  }
}