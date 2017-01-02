using System;


namespace EasyCredit.EasyCreditAssertionGroup
{
    public static class GeneralAssertionGroup
    {
        public static void ThrowIfArgumentIsNull<T>(this T obj, string argumentName = null)
            where T : class
        {
            if (obj == null)
            {
                if (argumentName != null)
                {
                    throw new ArgumentNullException(argumentName);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}
