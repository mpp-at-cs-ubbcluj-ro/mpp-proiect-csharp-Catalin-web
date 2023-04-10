using Microsoft.AspNet.SignalR.WebSockets;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    public class MyWebSocketHandler : WebSocketHandler
    {
        private static readonly List<MyWebSocketHandler> _handlers = new List<MyWebSocketHandler>();

        public MyWebSocketHandler(int? max) : base(max)
        {
            _handlers.Add(this);
        }

        public override void OnOpen()
        {
            // This method is called when a new WebSocket connection is established.
            _handlers.Add(this);
        }

        public override void OnMessage(string message)
        {
            // This method is called when a message is received from the client.
        }

        public override void OnClose()
        {
            _handlers.Remove(this);
        }

        public static void SendMessage(string message)
        {
            foreach (var handler in _handlers)
            {
                handler.Send(message);
            }
        }
    }
}
