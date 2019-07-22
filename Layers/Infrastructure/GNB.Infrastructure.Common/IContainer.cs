namespace GNB.Infrastructure.Common
{
    public interface IContainer
    {
        void InitializeAll();

        TService Resolve<TService>();
    }
}
