using BlindBoxSS.API.Modelss.Momo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Momo
{
    public interface IMomoService
    {
        //Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
        Task<string> CreatePaymentRequest(string amount, string orderId);

    }
}
