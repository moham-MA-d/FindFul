using Findful.Extentions.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Extentions.Common
{
    [TestFixture]
    public class DateTimeTests
    {
        private DateTime _dateTime = new DateTime(2001, 3, 4);
        private DateTimeExtentionHelper _dateTimeExtentionHelper;


        [SetUp] 
        public void SetUp()
        {
            _dateTimeExtentionHelper = new DateTimeExtentionHelper();
        }


        [Test]
        public void CalculateAge_WhenCalled_ReturnAge()
        {
            var d = _dateTimeExtentionHelper.GetCalculateAge(_dateTime);

            Assert.That(d, Is.LessThanOrEqualTo(80));
            
        }
    }
}
