using AutoFixture.Xunit2;
using Xunit;

namespace SKUT.Xunit
{
    /// <summary>
    /// Allows to provide explicit inline data in conjunction with <see cref="AutoSubstituteDataAttribute"/>.
    /// <example>
    /// <code>
    /// [Theory]<br/>
    /// [InlineAutoSubstituteData(2, "test")]<br/>
    /// private void TestMethod(int version, string company, IInterface i)
    /// </code>
    /// </example>
    /// </summary>
    public class InlineAutoSubstituteDataAttribute : CompositeDataAttribute
    {
        public InlineAutoSubstituteDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoSubstituteDataAttribute()) { }
    }
}