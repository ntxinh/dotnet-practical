using System;

namespace AST.Core.Common.Utils
{
    public static class ThrowIf
    {
        public static class Argument
        {
            public static void IsNull<T>(T argument)
            {
                if (argument is null)
                {
                    throw new ArgumentNullException(typeof(T).Name);
                }
            }
        }
    }
}
