using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace SKUT.Xunit
{
    /// <summary>
    /// Uses NSubstitute Autofixture to allow injection of auto data (i.e. auto-mocking) for test method parameters
    /// that contain mixed types such as interfaces and abstract classes, which Xunit's [InlineData], [AutoData], or [InlineAutoData]
    /// attributes do not support.
    /// <example>
    /// <code>
    /// [Theory]<br/>
    /// [AutoSubstituteData]<br/>
    /// private void TestMethod(IInterface i, AbstractType a)
    /// </code>
    /// </example>
    /// </summary>
    public class AutoSubstituteDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Constructs an instance of this attribute with user-definable array size count.
        /// </summary>
        /// <param name="fixtureRepeatCount">A user-definable array size count (default: 3).
        /// The following example will make the array parameter hold 7 instances of Type.
        /// <example>
        /// <code>
        /// [Theory]<br/>
        /// [AutoSubstituteData(7)]<br/>
        /// private void TestMethod(Type[] array)
        /// </code>
        /// </example></param>
        /// <param name="configureMembers"></param>
        public AutoSubstituteDataAttribute(int fixtureRepeatCount, bool configureMembers = true)
            : base(() => new Fixture() {RepeatCount = fixtureRepeatCount}.Customize(new AutoNSubstituteCustomization()
            {
                ConfigureMembers = configureMembers
            }))
        {
        }

        public AutoSubstituteDataAttribute() : this(3) { }
    }
}