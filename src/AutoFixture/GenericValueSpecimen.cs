using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace SKUT.AutoFixture
{
    /// <summary>
    /// A specimen builder implementation allowing to provide a custom constructor or property parameter
    /// (instead of auto-generated) for auto-mocked objects.<br/><br/>
    ///
    /// <example>
    /// <code>var fixture = new Fixture();<br/><br/>
    ///
    /// fixture.Customizations.Add(new GenericValueSpecimen&lt;DateTime&gt;(DateTime.Now));<br/><br/>
    /// var @event = fixture.Create&lt;DomainEventStub&gt;();
    /// </code>
    /// </example>
    /// </summary>
    public class GenericValueSpecimen<TId> : ISpecimenBuilder
    {
        private readonly bool mAlwaysCreateIfUnnamed;

        private readonly Func<TId> mValueAction;

        private GenericValueSpecimen(string valueName, bool alwaysCreateIfUnnamed)
        {
            if (string.IsNullOrWhiteSpace(valueName))
            {
                throw new ArgumentNullException(nameof(valueName));
            }

            mAlwaysCreateIfUnnamed = alwaysCreateIfUnnamed;
            Name                   = valueName;
        }

        /// <summary>
        /// Constructs a specimen builder to control parameters of type <see cref="TId"/>.
        /// </summary>
        /// <param name="value"></param>
        public GenericValueSpecimen(TId value)
        {
            Value = value;
        }

        public GenericValueSpecimen(Func<TId> value)
        {
            mValueAction = value;
        }

        /// <summary>
        /// Constructs a specimen builder to control parameters of type <see cref="TId"/>
        /// that have a specific parameter name.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueName"></param>
        /// <param name="alwaysCreateIfUnnamed"></param>
        public GenericValueSpecimen(TId value, string valueName, bool alwaysCreateIfUnnamed = false) :
            this(valueName, alwaysCreateIfUnnamed)
        {
            Value = value;
        }

        public GenericValueSpecimen(Func<TId> value, string valueName, bool alwaysCreateIfUnnamed = false) :
            this(valueName, alwaysCreateIfUnnamed)
        {
            mValueAction = value;
        }

        public TId Value { get; set; }

        public string Name { get; set; }

        public object Create(object request, ISpecimenContext context)
        {
            Type   targetType;
            string targetName = null;

            switch (request)
            {
                case ParameterInfo pi:
                {
                    targetType = pi.ParameterType;
                    targetName = pi.Name;
                    break;
                }
                case PropertyInfo pi:
                {
                    targetType = pi.PropertyType;
                    targetName = pi.Name;
                    break;
                }
                default:
                {
                    targetType = request as Type;
                    if (typeof(TId) == targetType)
                    {
                        break;
                    }
                    return new NoSpecimen();
                }
            }

            if (targetType != typeof(TId))
            {
                return new NoSpecimen();
            }

            if (string.IsNullOrWhiteSpace(Name) ||
                ((!string.IsNullOrWhiteSpace(targetName) || mAlwaysCreateIfUnnamed) &&
                 Name.Equals(targetName, StringComparison.OrdinalIgnoreCase)))
            {
                return (mValueAction is null) ? Value : mValueAction();
            }

            return new NoSpecimen();
        }
    }
}