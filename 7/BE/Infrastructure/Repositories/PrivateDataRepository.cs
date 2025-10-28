using HW7.Domain.Interfaces;

namespace HW7.Infrastructure.Repositories
{
    public class PrivateDataRepository : IPrivateDataRepository
    {
        public string GetPrivateData()
        {
            return "Henshin!";
        }
    }
}
