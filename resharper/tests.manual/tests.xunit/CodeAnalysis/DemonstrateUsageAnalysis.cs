﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace tests.xunit.eyeball.sourcecode
{
    namespace DemonstrateUsageAnalysis
    {
        // TEST: This namespace should be marked as in use
        namespace NamespaceContainingSingleClassMarkedAsUsed
        {
            // TEST: This class should be marked as in use
            public class SingleTestClass
            {
                // TEST: This method should be marked as in use
                [Fact]
                public void TestMethodMarkedAsInUse()
                {
                    Assert.Equal(1, 1);
                }
            }
        }

        // For completeness...
        namespace EmptyNamespaceMarkedAsNotInUse
        {
        }

        // TEST: Parent class should be marked as in use
        public class ParentClassMarkedAsInUse
        {
            // TEST: Nested class should be marked as in use
            // TEST: This class should get a test class marker
            public class NestedClassMarkedAsInUse
            {
                // TEST: This method should be marked as in use
                [Fact]
                public void TestMethodMarkedAsInUse()
                {
                    Assert.Equal(1, 1);
                }
            }
        }

        // TEST: This class should be marked as in use
        // TEST: This class should be flagged as a test class
        public class ClassInUse
        {
            // TEST: This method should be marked as in use
            [Fact]
            public void TestMethodMarkedInUse()
            {
            }
        }

        // TEST: This class should be marked as in use
        public class BaseClassFixtureMarkedAsInUse
        {
        }

        // TEST: This class should be marked as in use
        public class DerivedClassMarkedAsInUse : BaseClassFixtureMarkedAsInUse
        {
            [Fact]
            public void TestMethod()
            {
            }
        }

        public class PropertyDataTheoryTest
        {
            [Theory]
            [PropertyData("TheoryDataEnumerator")]
            public void DataFromProperty(int value)
            {
                Console.WriteLine("DataFromProperty({0})", value);
            }

            // TEST: This property should be marked as in use
            public static IEnumerable<object[]> TheoryDataEnumerator
            {
                // TEST: Getter should be marked as in use
                get { return Enumerable.Range(1, 10).Select(x => new object[] { x }); }
                // TEST: Setter should be marked as not in use
                set { var x = value; }
            }

            // TEST: This property should be marked as *NOT* in use
            public static IEnumerable<object[]> NotTheoryDataEnumerator
            {
                get { return null; }
            }
        }

        // TEST: Private class should not be marked in use - we don't have any valid tests
        class PrivateClassNotInUse
        {
            // TEST - FAIL: This should be marked as not in use, but it gets the implicit
            // usage attribute from the external annotations
            [Fact]
            public void NotReallyATestMethod()
            {
                Assert.False(false);
            }
        }

        public class DefaultConstructorNotSuggestedToBePrivate
        {
            // TEST: Default constructor should be marked as in use, with no suggestion that constructor can be made private
            // (When the class is in use, but no-one is using the constructor, it gets marked as "can be made private")
            // This only seems to be ReSharper 5?
            public DefaultConstructorNotSuggestedToBePrivate()
            {
                Console.WriteLine("Hello");
            }

            [Fact]
            public void Test()
            {
            }
        }

        public class DisposeMethodMarkedAsInUse : IDisposable
        {
            [Fact]
            public void Test()
            {
            }

            // TEST: Dispose is marked as in use
            public void Dispose()
            {
            }
        }

        public class FixtureData
        {
        }

        public class UseFixtureMethodsMarkedAsInUse : IUseFixture<FixtureData>
        {
            [Fact]
            public void Test()
            {
            }

            // TEST: SetFixture is marked as in use
            public void SetFixture(FixtureData data)
            {
            }
        }
    }
}