﻿using Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAO.Contracts.UserRequestAndResponse;

namespace Services.AccountService
{
    public interface IAccountService
    {
        Task<UserResponse> RegisterAsync(UserRegisterRequest request);
        Task<CurrentUserResponse> GetCurrentUserAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);
        Task DeleteAsync(Guid id);
        Task<RevokeRefreshTokenResponse> RevokeRefreshToken(RefreshTokenRequest refreshTokenRemoveRequest);
        Task<CurrentUserResponse> RefreshTokenAsync(RefreshTokenRequest request);

        Task<UserResponse> LoginAsync(UserLoginRequest request);

        Task<UserResponse> LoginGoogle(GoogleLoginRequest request);


        //Send Email
        //Task<bool> ConfirmEmailAsync(string userId, string token);
        Task<bool> ResendConfirmationEmailAsync(string email);
    }
}
