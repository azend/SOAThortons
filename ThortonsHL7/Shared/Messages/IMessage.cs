/*
 * FILE        : IMessage.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Class container for IMessage formatting.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class IMessage
    {
        protected const char BOM = '\xB';
        protected const char EOS = '\xD';
        protected const char EOM = '\x1C';
    }
}
