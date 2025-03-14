using BlindBoxSS.API.Services;
using Repositories.OrderRep;
using Repositories.Product;
using Repositories.UnitOfWork;
using Repositories.WalletRepo;
using Services;
using Services.AccountService;
using Services.Email;
using Services.OrderS;
using Services.Payment;
using Services.Product;
using Services.Wallet;

namespace BlindBoxSS.API.DI
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IWalletTransactionService, WalletTransactionService>();
            services.AddScoped<IBlindBoxRepository, BlindBoxRepository>();
            services.AddScoped<IBlindBoxService, BlindBoxService>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IBlindBoxImageService, BlindBoxImageService>();
            services.AddScoped<IPackageImageService, PackageImageService>();
        }
    }
}
