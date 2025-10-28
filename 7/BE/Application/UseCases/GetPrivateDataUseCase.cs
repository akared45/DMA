using HW7.Domain.Interfaces;

namespace HW7.Application.UseCases
{
    public class GetPrivateDataUseCase
    {
        private readonly IPrivateDataRepository _privateDataRepository;

        public GetPrivateDataUseCase(IPrivateDataRepository privateDataRepository)
        {
            _privateDataRepository = privateDataRepository;
        }
        public string Execute()
        {
            return _privateDataRepository.GetPrivateData();
        }
    }
}
