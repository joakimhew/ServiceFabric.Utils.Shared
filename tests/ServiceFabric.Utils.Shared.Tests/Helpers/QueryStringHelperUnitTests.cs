using ServiceFabric.Utils.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ServiceFabric.Utils.Shared.Tests.Helpers
{

    public class QueryStringHelperUnitTests
    {
        private class TestModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool? IsAlive { get; set; }
            public int? Age { get; set; }
            public decimal? Length { get; set; }
            public List<Guid> Ids { get; set; }
            public List<Guid?> NullableIds { get; set; }
            public SecondTestModel Nested { get; set; }
        }   

        private class SecondTestModel
        {
            public Guid? NestedId { get; set; }
        }
        

        [Fact]
        public void Parse_AllNulls_IsParsed()
        {
            var testModel = new TestModel();

            var expectedQueryString = "";
            var actualQueryString = QueryStringHelpers.Parse(testModel);

            Assert.Equal(expectedQueryString, actualQueryString);
        }

        [Fact]
        public void Parse_AllNonNulls_IsParsed()
        {
            var testId1 = Guid.NewGuid();
            var testId2 = Guid.NewGuid();
            var testId3 = Guid.NewGuid();
            var testId4 = Guid.NewGuid();

            var name = "testName";
            var isAlive = true;
            var age = 70;
            var length = (decimal) 123.2;

            var testModel = new TestModel
            {
                Id = testId1,
                Name = name,
                IsAlive = isAlive,
                Age = age,
                Length = length,
                Ids = new List<Guid>
                {
                    testId2,
                    testId3
                },
                Nested = new SecondTestModel
                {
                    NestedId = testId4
                }
            };

            var expectedString = $"id={testId1}&name={name}&isAlive={isAlive}&age={age}&length={length}&ids={testId2}&ids={testId3}&nestedId={testId4}";
            var actualString = QueryStringHelpers.Parse(testModel);

            Assert.Equal(expectedString, actualString);
        }

        [Fact]
        public void Parse_MixedNullsAndNonNulls_IsParsed()
        {
            var testId1 = Guid.NewGuid();
            var testId2 = Guid.NewGuid();
            var testId3 = Guid.NewGuid();

            var name = "testName";
            var isAlive = true;
            var length = (decimal)123.2;

            var testModel = new TestModel
            {
                Id = testId1,
                Name = name,
                IsAlive = isAlive,
                Length = length,
                Ids = new List<Guid>
                {
                    testId2,
                    testId3
                }
            };

            var expectedString = $"id={testId1}&name={name}&isAlive={isAlive}&length={length}&ids={testId2}&ids={testId3}";
            var actualString = QueryStringHelpers.Parse(testModel);

            Assert.Equal(expectedString, actualString);
        }
    }
}
