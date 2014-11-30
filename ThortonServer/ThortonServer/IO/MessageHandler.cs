using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThortonServer.Services;

namespace ThortonServer.IO
{
    class MessageHandler
    {
        private StateObject state;
        private ServiceManager sm;
        public MessageHandler(StateObject state, ServiceManager sm)
        {
            this.state = state;
            this.sm = sm;
        }

        public void Handle()
        {
            AbstractService service = sm.FindService(state);

            if (service != null)
            {
                service.Handle(state);
            }
            else
            {
                sm.ErrorService(state);
            }
        }

    }
}
