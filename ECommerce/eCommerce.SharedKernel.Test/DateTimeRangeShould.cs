using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.SharedKernel.Test
{
    [TestClass]
    public class DateTimeRangeShould
    {
        private readonly DateTime _testStartDate = new DateTime(2016, 1, 22, 9, 0, 0);

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowArgumentExceptionIfStartDateEqualsEndDate()
        {
            var dateTimeRange = new DateTimeRange(_testStartDate, _testStartDate);
        }
    }
}
