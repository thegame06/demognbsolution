using System;

namespace GNB.Infrastructure.Common
{
    public class FactoryContainer
    {
        private static readonly Lazy<FactoryContainer> instance = new Lazy<FactoryContainer>(() => new FactoryContainer());

        private IContainer _container;

        private FactoryContainer()
        {
            _container = new UnityContainerComponent();
            _container.InitializeAll();
        }

        public static FactoryContainer Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public IContainer Container
        {
            get
            {
                return _container;
            }
        }
    }
}
