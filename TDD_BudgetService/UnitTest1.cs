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
      var expected = 0;
      TotalBudgetShouldBe(expected, actual);
    }

    [Test]
    public void Period_Inside_Budget_Month()
    {
      _repo.GetAll().Returns(new List<Budget>() { new Budget() { YearMonth = "201904", Amount = 30 } });

      decimal actual = _service.Query(new DateTime(2019, 04, 01), new DateTime(2019, 04, 01));
      var expected = 1;
      TotalBudgetShouldBe(expected, actual);
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