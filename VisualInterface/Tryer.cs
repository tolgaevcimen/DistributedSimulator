using System;

namespace VisualInterface
{
    class Tryer
    {
        public static T Go<T>(Func<T> func, int times)
        {
            while (times > 0)
            {
                try
                {
                    return func();
                }
                catch
                {
                    if (--times <= 0)
                        throw;
                }
            }

            return default(T);
        }

        public static void Go(Action func, int times)
        {
            while (times > 0)
            {
                try
                {
                    func();
                }
                catch
                {
                    if (--times <= 0)
                        throw;
                }
            }
        }
    }
}
