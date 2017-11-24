using ServiceFabric.Utils.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ServiceFabric.Utils.Shared.Tests.Helpers
{
    public class QueryStringHelperUnitTests
    {
        [Fact]
        public void Parse_Object_HasMatchingString()
        {
            var testId1 = Guid.NewGuid();
            var testId2 = Guid.NewGuid();
            var testId3 = Guid.NewGuid();

            var time = DateTime.Now;
            var formattedTime = time.ToUniversalTime().ToString("o");

            TestModel test = new TestModel
            {
                Id = testId1,
                Ids = new List<Guid> { testId2, testId3 },
                Nested = new List<NestedTest> { new  NestedTest { Name = "testnest" } },
                IsSomething = true,
                Time = time
            };

            var expectedQueryString = $"?id={testId1}&ids={testId2}&ids={testId3}&name={test.Nested[0].Name}&isSomething=True&time={formattedTime}";
            var actualQueryString = $"?{QueryStringHelpers.Parse(test)}";

            Assert.Equal(expectedQueryString, actualQueryString);
        }

        private class TestModel
        {
            public TestModel()
            {
                Ids = new List<Guid>();
            }

            public Guid? Id { get; set; }
            public Guid? TestId { get; set; }
            public List<Guid> Ids { get; set; }
            public List<NestedTest> Nested { get; set; }
            public bool? IsSomething { get; set; }
            public DateTime Time { get; set; }
        }

        private class NestedTest
        {
            public string Name { get; set; }
        }
    }
}
