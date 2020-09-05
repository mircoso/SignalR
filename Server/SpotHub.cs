using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public delegate void ClientConnectionEventHandler(string clientId, string name);

    public delegate void CommandReceivedEventHandler(string senderClientId, string command);

    public class SpotHub : Hub<ISpot>
    {
        private static ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();

        public static event ClientConnectionEventHandler ClientConnected;

        public static event ClientConnectionEventHandler ClientDisconnected;

        public static event CommandReceivedEventHandler CommandReceived;

        public async Task OnlySpot()
        {
            await Clients.All.OnlySpot();
        }

        public override Task OnConnectedAsync()
        {
            _users.TryAdd(Context.ConnectionId, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name;

            _users.TryRemove(Context.ConnectionId, out name);

            ClientDisconnected?.Invoke(Context.ConnectionId, name);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SetName(string name)
        {
            await Task.Run(() =>
            {
                _users[Context.ConnectionId] = name;

                ClientConnected?.Invoke(Context.ConnectionId, name);
            });
        }

        //public async Task SendToUser(string user, string message)
        //{
        //    await Clients.User(user).SendAsync("ReceiveDirectMessage", $"{Context.UserIdentifier}: {message}");
        //}

        //public Task SendMessageToGroup(string groupName, string message)
        //{
        //    return Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId}: {message}");
        //}

        //public async Task AddToGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        //}
    }
}