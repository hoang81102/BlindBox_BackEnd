using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Services.Momo;

public class MomoService : IMomoService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MomoService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> CreatePaymentRequest(string amount, string orderId)
    {
        string partnerCode = _configuration["MomoAPI:PartnerCode"];
        string accessKey = _configuration["MomoAPI:AccessKey"];
        string secretKey = _configuration["MomoAPI:SecretKey"];
        string momoApiUrl = _configuration["MomoAPI:MomoApiUrl"];
        string returnUrl = _configuration["MomoAPI:ReturnUrl"];
        string notifyUrl = _configuration["MomoAPI:NotifyUrl"];
        string requestType = _configuration["MomoAPI:RequestType"];

        string requestId = Guid.NewGuid().ToString();
        string orderInfo = "Nạp tiền vào ví";
        string extraData = "";  // Có thể thêm thông tin bổ sung nếu cần

        // Tạo raw data string để ký
        string rawData = $"partnerCode={partnerCode}&accessKey={accessKey}&requestId={requestId}&amount={amount.ToString()}&orderId={orderId}" +
                  $"&orderInfo={orderInfo}&returnUrl={returnUrl}&notifyUrl={notifyUrl}&extraData={extraData}";

        // Tạo chữ ký (signature)
        string signature = CreateSignature(rawData, secretKey);

        // Tạo request body
        var requestBody = new
        {
            partnerCode,
            accessKey,
            requestId,
            amount,
            orderId,
            orderInfo,
            returnUrl,
            notifyUrl,
            requestType,
            extraData,
            signature
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // Gửi request đến MoMo
        HttpResponseMessage response = await _httpClient.PostAsync(momoApiUrl, content);
        string responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;
    }

    private string CreateSignature(string rawData, string secretKey)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower(); // Chuyển thành chữ thường
        }
    }

    public async Task<string> RefundTransaction(string orderId, decimal amount)
    {
        string partnerCode = _configuration["MomoAPI:PartnerCode"];
        string accessKey = _configuration["MomoAPI:AccessKey"];
        string secretKey = _configuration["MomoAPI:SecretKey"];
        string momoApiUrl = "https://test-payment.momo.vn/gw_payment/transactionProcessor";

        string requestId = Guid.NewGuid().ToString();
        string description = "Refund request";

        string rawData = $"partnerCode={partnerCode}&accessKey={accessKey}&orderId={orderId}&requestId={requestId}&amount={amount}&description={description}";
        string signature = CreateSignature(rawData, secretKey);

        var requestBody = new
        {
            partnerCode,
            accessKey,
            orderId,
            requestId,
            amount,
            description,
            signature,
            requestType = "refundMoMoWallet"
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(momoApiUrl, content);
        return await response.Content.ReadAsStringAsync();
    }


}
