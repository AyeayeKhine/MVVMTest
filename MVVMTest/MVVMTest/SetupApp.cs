using Autofac;
using GalaSoft.MvvmLight.Ioc;
using MVVMTest.Interfaces;
using MVVMTest.Models;
using MVVMTest.Services;

namespace MVVMTest
{
   public class SetupApp
    {
        private static SetupApp instance;
        /// <summary>
        /// This is a singleton instance for bootstraping the application.
        /// </summary>
        /// <value>The instance.</value>
        public static SetupApp Instance
        {
            get
            {
                if (instance == null)
                    instance = new SetupApp();

                return instance;
            }
        }

        public IContainer CreateContainer()
        {
            ContainerBuilder cb = new ContainerBuilder();

            Setup();

            return cb.Build();
        }

        /// <summary>
        /// Setup all injections
        /// </summary>
        public void Setup()
        {
            
            SimpleIoc.Default.Register<IRepository<User>, UserRepository>();
        }
    }
}
