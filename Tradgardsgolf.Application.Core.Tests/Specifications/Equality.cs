using FluentAssertions;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Application.Core.Tests.Specifications;

[TestFixture]
public class SpecificationEquality
{
    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldBeEqualByReference(TestSource source)
    {
        var isEqual = source.SourceObject.Equals(source.SourceObject);
        isEqual.Should().BeTrue();
    }

    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldBeEqualByEquality(TestSource source)
    {
        var isEqual = source.SourceObject.Equals(source.OtherObject);
        isEqual.Should().BeTrue();
    }

    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldNotBeEqualByDifferentTypes(TestSource source)
    {
        var isEqual = source.SourceObject.Equals("");
        isEqual.Should().BeFalse();
    }

    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldNotBeEqualWhenComparedWithNull(TestSource source)
    {
        var isEqual = source.SourceObject.Equals(null);
        isEqual.Should().BeFalse();
    }
    
    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldHaveSameHash(TestSource source)
    {
        source.SourceObject.GetHashCode().Should().Be(source.SourceObject.GetHashCode());
        source.SourceObject.GetHashCode().Should().Be(source.OtherObject.GetHashCode());
    }
    
    [Test]
    public void ShouldNotHaveSameHashCodeWhenDifferentType()
    {
        var guid = Guid.NewGuid();
        var spec1 = new Tradgardsgolf.Core.Specifications.Scorecard.ById(guid);
        var spec2 = new Tradgardsgolf.Core.Specifications.Course.ById(guid);
        
        spec1.GetHashCode().Should().NotBe(spec2.GetHashCode());
    }

    private class TestSources
    {
        private static readonly Guid Guid = Guid.NewGuid();

        public static IEnumerable<TestSource> Sources = CreateTestSources();

        private static IEnumerable<TestSource> CreateTestSources()
        {
            var specificationType = typeof(SpecificationEquatable<,>);
            var types = typeof(SpecificationEquatable<,>).Assembly.GetTypes().Where(t =>
                t is { IsClass: true, IsAbstract: false, BaseType.IsGenericType: true }
                && t.BaseType.GetGenericTypeDefinition() == specificationType);

            foreach (var type in types)
            {
                var constructor = type.GetConstructors().FirstOrDefault();
                if (constructor == null) continue;

                var parameters = constructor.GetParameters()
                    .Select(p => GetDefaultValue(p.ParameterType))
                    .ToArray();

                var sourceObject = Activator.CreateInstance(type, parameters);
                var otherObject = Activator.CreateInstance(type, parameters);

                yield return new TestSource(sourceObject, otherObject);
            }
        }

        private static object GetDefaultValue(Type type)
        {
            if (type == typeof(string)) return "test";
            if (type == typeof(int)) return 1;
            if (type == typeof(Guid)) return Guid.NewGuid();
            if (type == typeof(DateTime)) return DateTime.Now;
            if (type == typeof(bool)) return true;
            if (type == typeof(long)) return 1L;
            if (type == typeof(double)) return 1.0;
            if (type == typeof(decimal)) return 1m;
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0);
            if (type.IsValueType) return Activator.CreateInstance(type);
            return null;
        }
    }

    public class TestSource
    {
        public TestSource(object o, object otherObject)
        {
            SourceObject = o;
            OtherObject = otherObject;
        }

        public object SourceObject { get; set; }
        public object OtherObject { get; set; }
    }
}