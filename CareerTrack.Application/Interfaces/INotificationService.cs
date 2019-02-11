using CareerTrack.Application.Notifications.Models;
using System.Threading.Tasks;

namespace CareerTrack.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
