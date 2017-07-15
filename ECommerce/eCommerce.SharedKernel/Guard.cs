using System;
using System.Linq.Expressions;

namespace eCommerce.SharedKernel
{
    public class Guard
    {
        public static void ForLessEqualZero(ParameterInfo<int> param)
        {
            if (param.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(param.Name);
            }
        }

        public static void ForPrecedesDate(ParameterInfo<DateTime> param, DateTime dateToPrecede)
        {
            if (param.Value >= dateToPrecede)
            {
                throw new ArgumentOutOfRangeException(param.Name);
            }
        }

        public static void ForNullOrEmpty(ParameterInfo<string> param)
        {
            if (string.IsNullOrEmpty(param.Value))
            {
                throw new ArgumentOutOfRangeException(param.Name);
            }
        }
    }
}
