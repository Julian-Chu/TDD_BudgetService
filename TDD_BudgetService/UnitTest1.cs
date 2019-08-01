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
      TotalBudgetShouldBe(start, end);
    }

    private void TotalBudgetShouldBe(DateTime start, DateTime end)
    {
      decimal actual = service.Query(start, end);
      var expected = 0;
      Assert.AreEqual(expected, actual);
    }
  }

  public class BudgetService
  {
    public decimal Query(DateTime start, DateTime end)
    {
      return 0;
    }
  }
}