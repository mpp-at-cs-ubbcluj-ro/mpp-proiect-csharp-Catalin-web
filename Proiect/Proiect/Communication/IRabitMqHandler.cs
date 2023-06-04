using System;

namespace Proiect.Communication
{
    public interface IRabitMqHandler
    {
        void sendMessage(string message);
        void addCallback(Action callback);
    }
}
