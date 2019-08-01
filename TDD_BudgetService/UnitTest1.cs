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
      var budgets = new List<Budget>() { };
      _repo.GetAll().Returns(budgets);
      var start = new DateTime(2019, 04, 01);
      var end = new DateTime(2019, 04, 03);
      decimal actual = _service.Query(start, end);
      var expected = 0;
      TotalBudgetShouldBe(expected, actual);
    }

    [Test]
    public void Period_Inside_Budget_Month()
    {
      var budgets = new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } };
      _repo.GetAll().Returns(budgets);

      var start = new DateTime(2019, 04, 01);
      var end = new DateTime(2019, 04, 01);
      decimal actual = _service.Query(start, end);
      var expected = 1;
      TotalBudgetShouldBe(expected, actual);
    }

    private void TotalBudgetShouldBe(int expected, decimal actual)
    {
      Assert.AreEqual(expected, actual);
    }
  }

  public interface IBudgetRepository
  {
    List<Budget> GetAll();
  }
}