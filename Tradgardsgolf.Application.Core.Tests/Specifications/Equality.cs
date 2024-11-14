using FluentAssertions;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Application.Core.Tests.Specifications;

[TestFixture]
public class SpecificationEquality
{
    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldBeEqualByReference(TestSource source)
    {
        // ReSharper disable once EqualExpressionComparison
        var isEqual = source.SourceObject.Equals(source.SourceObject);
        isEqual.Should().BeTrue();
    }

    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldBeEqualByEquality(TestSource source)
    {
        var isEqual = source.SourceObject.Equals(source.SameAsSourceObject);
        isEqual.Should().BeTrue();
    }

    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldNotBeEqualByDifferentTypes(TestSource source)
    {
        var isEqual = source.SourceObject.Equals("");
        isEqual.Should().BeFalse();
    }
    
    [TestCaseSource(typeof(TestSources), nameof(TestSources.Sources))]
    public void ShouldNotBeEqualComparedWithOtherObject(TestSource source)
    {
        var isEqual = source.SourceObject.Equals(source.OtherObject);
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
        source.SourceObject.GetHashCode().Should().Be(source.SameAsSourceObject.GetHashCode());
    }
    
    [Test]
    public void ShouldNotHaveSameHashCodeWhenDifferentType()
    {
        var guid = Guid.NewGuid();
        var spec1 = Specs.ById<Scorecard>(guid);
        var spec2 = Specs.ById<Course>(guid);
        
        spec1.GetHashCode().Should().NotBe(spec2.GetHashCode());
    }

    private class TestSources
    {
        public static IEnumerable<TestSource> Sources = CreateTestSources();

        private static IEnumerable<TestSource> CreateTestSources()
        {
            var specificationType = typeof(SpecificationEquatable<,>);
            var types = typeof(SpecificationEquatable<,>).Assembly.GetTypes().Where(t =>
                t is { IsClass: true, IsAbstract: false, BaseType.IsGenericType: true }
                && t.BaseType.GetGenericTypeDefinition() == specificationType
                && t.ContainsGenericParameters == false);

            foreach (var type in types)
            {
                var constructor = type.GetConstructors().FirstOrDefault();
                if (constructor == null) continue;

                var parameters = constructor.GetParameters()
                    .Select(p => GetDefaultValue(p.ParameterType))
                    .ToArray();
                
                var otherParameters = constructor.GetParameters()
                    .Select(p => GetOtherValue(p.ParameterType))
                    .ToArray();

                var sourceObject = Activator.CreateInstance(type, parameters) ?? throw new InvalidOperationException($"Cannot create an instance of {type.Name}");
                var sameObject = Activator.CreateInstance(type, parameters) ?? throw new InvalidOperationException($"Cannot create an instance of {type.Name}");
                var otherObject = Activator.CreateInstance(type, otherParameters) ?? throw new InvalidOperationException($"Cannot create an instance of {type.Name}");

                yield return new TestSource(sourceObject, sameObject, otherObject);
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
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0)!;
            if (type.IsValueType) return Activator.CreateInstance(type) ?? throw new InvalidOperationException($"Cannot create default value for type {type.FullName}");

            throw new InvalidOperationException($"Cannot create default value for type {type.FullName}");
        }
        
        private static object GetOtherValue(Type type)
        {
            if (type == typeof(string)) return "test1";
            if (type == typeof(int)) return 11;
            if (type == typeof(Guid)) return Guid.NewGuid();
            if (type == typeof(DateTime)) return DateTime.Now.AddDays(1);
            if (type == typeof(bool)) return false;
            if (type == typeof(long)) return 11L;
            if (type == typeof(double)) return 11.0;
            if (type == typeof(decimal)) return 11m;
            if (type.IsEnum) return Enum.GetValues(type).GetValue(Enum.GetValues(type).Length)!;
            if (type.IsValueType) return Activator.CreateInstance(type) ?? throw new InvalidOperationException($"Cannot create default value for type {type.FullName}");
            
            throw new InvalidOperationException($"Cannot create default value for type {type.FullName}");
        }
    }

    public class TestSource(object sourceObject, object sameAsSourceObject, object otherObject)
    {
        public object OtherObject { get; } = otherObject;
        public object SourceObject { get; } = sourceObject;
        public object SameAsSourceObject { get; } = sameAsSourceObject;
    }
}