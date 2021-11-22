using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class DashboardFabricaService : IDashboardFabricaService
    {

        //await _hubContext.Clients.All.SendAsync("FabricaDashboardUpdate");
    }
}
