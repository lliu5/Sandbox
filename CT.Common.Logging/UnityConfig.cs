using System;
using Microsoft.Practices.Unity;
using CT.Common.Logging.Interfaces;

namespace CT.Common.Logging
{
    public static class UnityConfig
    {

        /// <summary>
        /// Static constructor for DependencyFactory which will 
        /// initialize the unity container.
        /// </summary>
        public static IUnityContainer Container => LazyContainer.Value;

        private static readonly Lazy<IUnityContainer> LazyContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            Register(container);
            return container;
        });

        private static void Register(IUnityContainer container)
        {

            // Logging
           // container.RegisterType<ILogger, AptsCommonLogger>();

            //ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }
    }
}
