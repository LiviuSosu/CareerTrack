using CareerTrack.Application.Interfaces;
using CareerTrack.Application.Notifications.Models;
using System.Threading.Tasks;

namespace CareerTrack.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
