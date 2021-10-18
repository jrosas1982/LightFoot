using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Core.Aplicacion.Hubs
{
    public class NotificationsHub : Hub
    {
        //TODO mejorar
        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
        }

        public async Task OrdenProduccionAddGroup(string idOrdenProduccion)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, idOrdenProduccion);
        }

        public async Task OrdenProduccionRemoveGroup(string idOrdenProduccion)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idOrdenProduccion);
        }

    }
}
