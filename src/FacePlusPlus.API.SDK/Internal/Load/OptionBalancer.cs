using System;
using System.Collections.Generic;
using System.Threading;

namespace FacePlusPlus.API.SDK.Internal.Load
{
    internal class OptionBalancer<T> : IOptionBalancer<T>
    {
        protected int Index = -1;

        public IReadOnlyList<T> Pool { get; }

        public OptionBalancer(IReadOnlyList<T> pool)
        {
            if (pool == null || pool.Count == 0)
            {
                throw new ArgumentException("pool can not be empty");
            }
            Pool = pool;
        }

        public T Next()
        {
            Interlocked.Increment(ref Index);
            return Pool[Index % Pool.Count];
        }

        public int Count => Pool.Count;
    }
}