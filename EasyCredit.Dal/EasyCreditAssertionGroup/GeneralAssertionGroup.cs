using System;

namespace EasyCredit.DAL.EasyCreditAssertionGroup
{
    public class GeneralAssertionGroup<T> where T : class
    {
        public static void ValueCanNotBeNull(T value)
        {
            if (value == null)
                throw new ArgumentNullException(string.Format("type {0} can not be null in this case", value.GetType()));
        }
    }
}

