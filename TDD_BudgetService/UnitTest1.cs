using System;
using NUnit.Framework;

namespace Tests
{
  public class BudgetServiceTests
  {
    private BudgetService service;

    [SetUp]
    public void Setup()
    {
      service = new BudgetService();
    }

    [Test]
    public void No_Budget()
    {
      var start = new DateTime(2019, 04, 01);
      var end = new DateTime(2019, 04, 03);
      decimal actual = service.Query(start, end);
      var expected = 0;
      TotalBudgetShouldBe(expected, actual);
    }

    [Test]
    public void Period_Inside_Budget_Month()
    {
      var start = new DateTime(2019, 04, 01);
      var end = new DateTime(2019, 04, 01);
      decimal actual = service.Query(start, end);
      var expected = 1;
      TotalBudgetShouldBe(expected, actual);
    }

    private void TotalBudgetShouldBe(int expected, decimal actual)
    {
      Assert.AreEqual(expected, actual);
    }
  }
}