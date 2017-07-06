using JetBrains.Application;
using JetBrains.DataFlow;
using JetBrains.Threading;

namespace SampleReSharperPlugin
{
    public class Worker
    {
        public IProperty<long> Result;

        public Worker(Lifetime lifetime, IThreading threading, IShellLocks shellLocks)
        {
            // under construction                        
        }


        // Here can be some real calculations. We imitate them with finding a prime number.
        public long Calculate(int number)
        {
            var count = 0;
            long a = 2;
            while (count < number)
            {
                long b = 2;
                var prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)                
                    count++;                
                a++;
            }
            return (--a);
        }

    }
}