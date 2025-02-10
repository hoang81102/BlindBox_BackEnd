using BlindBoxSS.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Momo;
using System;
using System.Threading.Tasks;

[Route("api/momo")]
[ApiController]
public class MomoController : ControllerBase
{
    private readonly IMomoService _momoService;

    public MomoController(IMomoService momoService)
    {
        _momoService = momoService;
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] MomoDepositRequest request)
    {
        if (request.Amount <= 0)
        {
            return BadRequest("Số tiền nạp không hợp lệ.");
        }

        string orderId = Guid.NewGuid().ToString(); // Tạo Order ID duy nhất
        string response = await _momoService.CreatePaymentRequest(request.Amount.ToString(), orderId);

        return Ok(response);
    }
}


