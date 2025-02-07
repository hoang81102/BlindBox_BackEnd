using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BlindBoxSV
{
     public interface IBlindBoxService
    {
        BlindBox CreatePackage(BlindBox blindBox);
        void RemovePackage(BlindBox blindBox);
        List<BlindBox> GetAllBlindBoxs();
        BlindBox GetBlindBoxById(int Id);
        BlindBox UpdateBlindBox(BlindBox blindBox);
    }
}
