using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dawnx
{
    public abstract class Cached<TModel>
    {
        private TimeSpan CachePeriod;
        public Cached(TimeSpan cachePeriod)
        {
            CachePeriod = cachePeriod;
        }

        protected ReaderWriterLockSlim ReadWriteLock = new ReaderWriterLockSlim();
        public object UpdateLock = new object();

        public TModel CachedModel { get; protected set; }
        public DateTime ExpireTime { get; protected set; }

        public bool IsExpired => DateTime.Now > ExpireTime;

        public abstract TModel CacheNewModel();

        public void Cache()
        {
            try
            {
                ReadWriteLock.EnterWriteLock();
                CachedModel = CacheNewModel();
                ExpireTime = DateTime.Now.Add(CachePeriod);
            }
            finally
            {
                ReadWriteLock.ExitWriteLock();
            }
        }

        public TModel Get()
        {
            if (IsExpired)
            {
                lock (UpdateLock)
                {
                    if (IsExpired)
                        Cache();
                }
            }

            try
            {
                ReadWriteLock.EnterReadLock();
                return CachedModel;
            }
            finally
            {
                ReadWriteLock.ExitReadLock();
            }
        }

    }
}
