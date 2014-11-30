using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonServer.IO;

namespace ThortonServer.Services
{
    public class ServiceManager
    {

        private List<AbstractService> services = new List<AbstractService>();
        private AbstractService errorService = null;

        public ServiceManager(bool autoLoad = false)
        {
            if (autoLoad) {
                LoadServices();
            }
        }

        public void LoadServices()
        {
            services.Add(new RegisterTeamService());
            services.Add(new HelloWorldService());

            AbstractService errorService = new ErrorService();
            services.Add(errorService);
            this.errorService = errorService;
        }

        public IEnumerable<AbstractService> GetAllServices()
        {
            return services;
        }

        public AbstractService FindService(StateObject state)
        {
            AbstractService foundService = null;

            foreach (AbstractService service in services)
            {
                if (service.CanHandle(state))
                {
                    foundService = service;
                    break;
                }
            }

            return foundService;
        }

        public void ErrorService(StateObject state)
        {
            errorService.Handle(state);
        }



    }
}
