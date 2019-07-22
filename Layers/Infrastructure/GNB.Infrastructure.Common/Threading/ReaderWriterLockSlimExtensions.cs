using System.Threading;

namespace GNB.Infrastructure.Common.Threading
{
    public static class ReaderWriterLockSlimExtensions
    {
        public static ReaderSlimSync ReaderLock(
            this ReaderWriterLockSlim readerWriterLock)
        {
            return new ReaderSlimSync(readerWriterLock);
        }
    }
}
