using System.Reflection;
using AutoFixture.Kernel;

namespace SKUT.AutoFixture
{
    /// <summary>
    /// A specimen for a user-defined numerical range [min-max]
    /// </summary>
    /// <typeparam name="TNumeric"></typeparam>
    public class RangeSpecimen<TNumeric> : ISpecimenBuilder
    {
        private readonly TNumeric mMin;
        private readonly TNumeric mMax;
        public RangeSpecimen(TNumeric min, TNumeric max)
        {
            mMin = min;
            mMax = max;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (!(request is PropertyInfo pi) || pi.PropertyType != typeof(TNumeric))
            {
                return new NoSpecimen();
            }
            return context.Resolve(new RangedNumberRequest(typeof(TNumeric), mMin, mMax));
        }
    }
}