using Model.Models;
using DAO;

namespace Repository.BlindBoxRepo
{
    public class BlindBoxRepo : IBlindBoxRepo
    {
        private readonly BlindBoxDBContext _context;

        public BlindBoxRepo(BlindBoxDBContext context)
        {
            _context = context;
        }

        BlindBox IBlindBoxRepo.CreatePackage(BlindBox blindBox)
        {
            _context.BlindBoxes.Add(blindBox);
            _context.SaveChanges();
            return blindBox;
        }

        List<BlindBox> IBlindBoxRepo.GetAllBlindBoxs()
        {
            return _context.BlindBoxes.ToList();
        }

        BlindBox IBlindBoxRepo.GetBlindBoxById(int Id)
        {
            return _context.BlindBoxes.FirstOrDefault(p => p.BlindBoxId == Id);
        }

        void IBlindBoxRepo.RemovePackage(BlindBox blindBox)
        {
            _context.BlindBoxes.Remove(blindBox);
            _context.SaveChanges();
        }

        BlindBox IBlindBoxRepo.UpdateBlindBox(BlindBox blindBox)
        {
            _context.Entry(blindBox).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return blindBox;
        }
    }
}
