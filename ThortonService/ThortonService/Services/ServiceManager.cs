using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService.Services
{
    class ServiceManager
    {
        static List<String> serviceList = new List<string>();


        static private string currentNamespace = String.Empty;

        static ServiceManager()
        {
            MethodBase current = System.Reflection.MethodBase.GetCurrentMethod();
            currentNamespace = current.DeclaringType.Namespace;
            serviceList.Add(currentNamespace + ".PurchaseTotallerService");
        }


        public static ServiceData[] getServiceData()
        {
            List<ServiceData> args = new List<ServiceData>();
            ServiceInterface item;
            foreach (String service in serviceList)
            {
                item = (AbstractService)Activator.CreateInstance(Type.GetType(service));
                args.Add(item.getData());
            }

            //TODO I AM WORKING HERE RIGHT NOW!!!!!
            return args.ToArray();

        }

        public static ServiceArgument[] getArguments()
        {
            List<ServiceArgument> args = new List<ServiceArgument>();
            /*ServiceInterface item;
            foreach (String service in serviceList)
            {
                item = (AbstractService)Activator.CreateInstance(Type.GetType(service));
                args.Add(item.getArgs());
            }*/

            //TODO I AM WORKING HERE RIGHT NOW!!!!!
            return args.ToArray();
        }


        static ServiceInterface getService(String name)
        {
            ServiceInterface item;
            foreach (String service in serviceList)
            {
                item = (AbstractService)Activator.CreateInstance(Type.GetType(service));
                if (item.serviceName == name)
                {
                    return item;
                }
            }

            return null;
        }





    }
}
