using GNB.Infrastructure.Common.Logging;
using GNB.Infrastructure.Common.Threading;
using System;
using System.Threading;

namespace GNB.Infrastructure.Common
{
    public static partial class Capacity
    {
        static Lazy<LogWriterFacades> _logWriterFacades;
        static ReaderWriterLockSlim _syncLogger = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        static Capacity()
        {
            InitializeCapacities();
        }

        static void InitializeCapacities()
        {
            _logWriterFacades = new Lazy<LogWriterFacades>(() => new LogWriterFacades(), true);
        }


        public static LogWriterFacades LogWriterFacades
        {
            get
            {
                using (_syncLogger.ReaderLock())
                    return _logWriterFacades.Value;
            }
        }
    }
}

