using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Extensions;

namespace tests.xunitversions
{
    public class Theories
    {
        [Theory]
        [InlineData("hello")]
        [InlineData("world")]
        public void TheoryTest(string value)
        {
            Assert.Equal(5, value.Length);
        }
   }
}