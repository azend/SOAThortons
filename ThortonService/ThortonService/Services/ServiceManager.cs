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
        static String teamName = String.Empty;
        static Int32 teamID;

        static private string currentNamespace = String.Empty;
        
        static ServiceManager()
        {
            MethodBase current = System.Reflection.MethodBase.GetCurrentMethod();
            currentNamespace = current.DeclaringType.Namespace;
        }




        static ServiceInterface getService(String name)
        {
            ServiceInterface item;
            foreach(String service in serviceList)
            {
                item = (AbstractService)Activator.CreateInstance(Type.GetType(service));
                if(item.serviceName == name)
                {
                    return item;
                }
            }

            return null;
        }
            
            
         
        

    }
}
