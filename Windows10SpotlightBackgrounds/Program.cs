using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Windows10SpotlightBackgrounds
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string name = ".Windows10SpotlightBackgrounds";
                const string description = "Windows 10 Spotlight Backgrounds";

                var host = HostFactory.New(configuration =>
                {
                    configuration.Service<Host>(s =>
                    {
                        s.ConstructUsing(atlasHost => new Host());
                        s.WhenStarted(i => i.Start());
                        s.WhenStopped(i => i.Stop());
                    });
                    configuration.SetDescription(name);
                    configuration.SetServiceName(name);
                    configuration.SetDescription(description);
                    configuration.RunAsLocalSystem();
                });

                host.Run();
            }
            catch (Exception ex)
            {
            }
        }
    }
}