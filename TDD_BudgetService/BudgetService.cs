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
      var budgets = _repo.GetAll();
      if (budgets.Count == 0)
      {
        return 0;
      }


      var queryBudgets = budgets.Where(b => !period.IsNoOverlapping(b.Period));
      decimal sum = 0;
      foreach (var budget in queryBudgets)
      {
        var budgetPeriod = budget.Period;
        sum += OverlappingDays(period, budgetPeriod) * budget.dailyAmount;
      }
      return sum;
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