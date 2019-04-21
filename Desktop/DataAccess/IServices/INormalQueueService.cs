using Entities.Models;

namespace DataAccess.IServices
{
    public interface INormalQueueService
    {
        void Create(QueuePhoneNumber call);

        QueuePhoneNumber GetNextNumber();
    }
}
