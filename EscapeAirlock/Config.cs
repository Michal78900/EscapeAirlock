using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace EscapeAirlock
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}
