using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Mono.MvvmLightPlus
{
    public class Bootstrapper
    {
        private Bootstrapper() { }
        private static readonly Bootstrapper _instance = new Bootstrapper();
        internal static Bootstrapper Instance { get { return _instance; } }
        internal CompositionHost CompositionHost { get; set; }

        public static void Configure(Type app)
        {
            var configuration = new ContainerConfiguration()
                    .WithAssembly(app.GetTypeInfo().Assembly);
            _instance.CompositionHost = configuration.CreateContainer();
        }
    }
}
