using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SMSApi;
using SMSModels;
using SMSService_Demo2.Interfaces;
using SMSService_Demo2.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace SMSService_Demo2.Provider
{
    public class SMSProvider : ISMSProvider
    {
        private CompositionHost compContainer;

        // This is used in the listing of all assemblies (commented out below)
        [ImportMany]
        public IEnumerable<IMessage> MessageServices { get; private set; }

        // this is for loading the single assembly
        // the import attribute seems to be unnecessary --> TO DO: figure out.
        [Import]
        public IMessage MessageService { get; private set; }

        public SMSProvider(){} //SMSProvider(){Compose();}
       
        [HttpPost]
        public async Task<ActionResult<SMSResponse>> SendSingleMessage<T>(T rawRequest)
        {
            try
            {
                ValuesService valSvc = new ValuesService();
                IDictionary<string, string> inputParms = valSvc.GetCollectionFromRaw(rawRequest.ToString().Split("\r\n"), ":");
                string service = inputParms["Service"].ToString();

                Compose(service);

                return await MessageService.SendMessageAsync(inputParms);
            }
            catch (Exception ex)
            {
                SMSResponse resp = new SMSResponse()
                {
                    DiagnosticInformation = string.Empty,
                    ErrorFlag = true,
                    ErrorID = 1,
                    ErrorMessage = ex.Message
                };

                return new ObjectResult(resp);
            }
            finally
            {
                // get rid of the container when done --> tidy up, son
                //compContainer.Dispose();
                GC.Collect();
            }
        }

        private void Compose(string service)
        {
            try
            {
                // set the path to the plugins directory
                string path = "C:\\Users\\david.renz\\Documents\\_work\\Projects\\SMS\\code_demo\\SMSService_Demo2\\plugins\\" + service + ".dll";

                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                Type type = assembly.GetType(assembly.DefinedTypes.First().FullName);

                // Method one --> using the container
                ContainerConfiguration config = new ContainerConfiguration().WithAssembly(assembly);
                compContainer = config.CreateContainer();
                MessageService = compContainer.GetExport<IMessage>();

                // Method two --> without the container
                //MessageService = Activator.CreateInstance(type) as IMessage;
            }
            catch { throw new Exception("Could not load selected service."); }
        }

        [HttpPost]
        public async Task<IEnumerable<(string recipientNumber, string message)>> SendMessageForAllServices()
        {
            //return MessageServices.Select(f => new { Id = f.GetType().ToString(), Value = f.SendMessage("1-541-111-2222", "This is a test").ToString() })
            //    .AsEnumerable()
            //    .Select(c => (c.Id, c.Value))
            //    .ToList();

            return null;
        }
    }
}



// Extra stuff Below




// DJR --> old stuff
// this is a way to list out all of the assemblies --> vaguely interesting
//private void Compose()
//{
//    var currentDir = Directory.GetCurrentDirectory();

//    // Catalogs does not exists in Dotnet Core, so you need to manage your own.
//    var assemblies = new List<Assembly>() { typeof(Program).GetTypeInfo().Assembly };

//    DJR --> note:: the path below was from a Docker containe (C:/app/bin). .. change if not running in Docker!
//    var pluginAssemblies = Directory.GetFiles("C:/app/bin/plugins", "*.dll", SearchOption.TopDirectoryOnly)
//        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
//        // Ensure that the assembly contains an implementation for the given type.
//        .Where(s => s.GetTypes().Where(p => typeof(IMessage).IsAssignableFrom(p)).Any());
//    assemblies.AddRange(pluginAssemblies);


//    DJR --> wait a miute ... show me which assemblies we are talking about.
//    foreach (Assembly a in pluginAssemblies)
//    {
//        var full = a.FullName;
//        var manifestName = a.ManifestModule.Name;
//        var getName = a.GetName(true);
//    }


//    var configuration = new ContainerConfiguration()
//        .WithAssemblies(assemblies);
//    using (var container = configuration.CreateContainer())
//    {
//        MessageServices = container.GetExports<IMessage>();
//    }
//}