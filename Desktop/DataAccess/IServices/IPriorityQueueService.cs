using Entities.Models;

namespace DataAccess.IServices
{
    public interface IPriorityQueueService
    {
        void Create(QueuePhoneNumber call);

        QueuePhoneNumber GetNextNumber();
    }
}
