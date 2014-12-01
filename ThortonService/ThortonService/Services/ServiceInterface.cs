using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService.Services
{
    interface ServiceInterface
    {
        string serviceName{get;}

        string Process(string command);

        ServiceArgument[] getArgs();

        ServiceResponses[] getResp();
    }
}
