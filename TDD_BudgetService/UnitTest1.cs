using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
  public class BudgetServiceTests
  {
    private BudgetService _service;

    private IBudgetRepository _repo = Substitute.For<IBudgetRepository>();

    [SetUp]
    public void Setup()
    {
      _service = new BudgetService(_repo);
    }

    [Test]
    public void No_Budget()
    {
      _repo.GetAll().Returns(new List<Budget>() { });
      decimal actual = _service.Query(new DateTime(2019, 04, 01), new DateTime(2019, 04, 03));
      TotalBudgetShouldBe(0, actual);
    }

    [Test]
    public void Period_Inside_Budget_Month()
    {
      _repo.GetAll().Returns(new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } });

      decimal actual = _service.Query(new DateTime(2019, 04, 01), new DateTime(2019, 04, 01));
      TotalBudgetShouldBe(1, actual);
    }

    [Test]
    public void period_no_overlap_before_budget_first_day()
    {
      _repo.GetAll().Returns(new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } });

      decimal actual = _service.Query(new DateTime(2019, 3, 31), new DateTime(2019, 03, 31));
      TotalBudgetShouldBe(0, actual);
    }

    [Test]
    public void period_no_overlap_after_budget_last_day()
    {
      _repo.GetAll().Returns(new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } });

      decimal actual = _service.Query(new DateTime(2019, 5, 01), new DateTime(2019, 5, 01));
      TotalBudgetShouldBe(0, actual);
    }

    [Test]
    public void period_overlap_budget_last_day()
    {
      _repo.GetAll().Returns(new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } });

      decimal actual = _service.Query(new DateTime(2019, 4, 30), new DateTime(2019, 5, 01));
      TotalBudgetShouldBe(1, actual);
    }

    [Test]
    public void period_cross_budget_month()
    {
      _repo.GetAll().Returns(new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } });

      decimal actual = _service.Query(new DateTime(2019, 3, 31), new DateTime(2019, 5, 01));
      TotalBudgetShouldBe(30, actual);
    }

    [Test]
    public void invalid_period()
    {
      decimal actual = _service.Query(new DateTime(2019, 4, 02), new DateTime(2019, 04, 01));
      TotalBudgetShouldBe(0, actual);
    }

    private void TotalBudgetShouldBe(decimal expected, decimal actual)
    {
      Assert.AreEqual(expected, actual);
    }
  }

  public interface IBudgetRepository
  {
    List<Budget> GetAll();
  }
}