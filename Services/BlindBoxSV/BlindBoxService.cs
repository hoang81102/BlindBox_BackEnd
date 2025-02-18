using Model.Models;
using Repository.BlindBoxRepo;

namespace Services.BlindBoxSV
{
    public class BlindBoxService : IBlindBoxService
    {
        private readonly IBlindBoxRepo _blindBoxRepo;

        public BlindBoxService(IBlindBoxRepo blindBoxRepo)
        {
            _blindBoxRepo = blindBoxRepo;
        }
        BlindBox IBlindBoxService.CreatePackage(BlindBox blindBox)
        {
            return _blindBoxRepo.CreatePackage(blindBox);
        }
        List<BlindBox> IBlindBoxService.GetAllBlindBoxs()
        {
            return _blindBoxRepo.GetAllBlindBoxs();
        }
        BlindBox IBlindBoxService.GetBlindBoxById(int Id)
        {
            return _blindBoxRepo.GetBlindBoxById(Id);
        }
        void IBlindBoxService.RemovePackage(BlindBox blindBox)
        {
            _blindBoxRepo.RemovePackage(blindBox);
        }
        BlindBox IBlindBoxService.UpdateBlindBox(BlindBox blindBox)
        {
            return _blindBoxRepo.UpdateBlindBox(blindBox);
        }
    }
}
