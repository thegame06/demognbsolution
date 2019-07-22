using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace GNB.Infrastructure.Common.Threading
{
    public sealed class ReaderSlimSync : IDisposable
    {
        readonly ReaderWriterLockSlim _readerWriterLock;

        public ReaderSlimSync(
            ReaderWriterLockSlim readerWriterLock)
        {
            readerWriterLock.EnterReadLock();
            _readerWriterLock = readerWriterLock;
        }

        #region IDisposable pattern implementation

        int _disposed;

        public bool IsDisposed
        {
            get { return Volatile.Read(ref _disposed) != 0; }
        }


        public void Dispose()
        {
            if (Interlocked.Exchange(ref _disposed, 1) == 0)
                _readerWriterLock.ExitReadLock();
        }
        #endregion
    }
}
