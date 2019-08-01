using System;
using NUnit.Framework;

namespace Tests
{
  public class BudgetServiceTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void No_Budget()
    {
      var service = new BudgetService();
      var start = new DateTime(2019, 04, 01);
      var end = new DateTime(2019, 04, 03);
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