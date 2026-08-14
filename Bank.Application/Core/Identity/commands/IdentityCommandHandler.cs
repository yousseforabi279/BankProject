using Bank.Application.common.Results;
using Bank.Application.contracts;
using Bank.Application.Core.Identity.commands.Login;
using Bank.Application.Core.Identity.commands.Register;
using Bank.Application.Core.Identity.Responses.Register;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Core.Identity.commands
{
    public class IdentityCommandHandler : IRequestHandler<registercommand, Result<RegisterResponse>>,
                                          IRequestHandler<logincommand, Result<RegisterResponse>>
    {
        private readonly IUnityOfWork _unityOfWork;

        public IdentityCommandHandler(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public async Task<Result<RegisterResponse>> Handle(registercommand request, CancellationToken cancellationToken)
        {
            var exists = await _unityOfWork.identityRepo.UserExistsAsync(request.Email!);
            if (exists)
            {
                return Result<RegisterResponse>.Failure(
                    ResultStatus.Conflict,
                    "Email is already registered.");
            }

            var created = await _unityOfWork.identityRepo.CreateUserAsync(
                                                                request.UserName!,
                                                                request.Email!,
                                                                request.Password!);
            if (!created)
            {
                return Result<RegisterResponse>.Failure(
                    ResultStatus.BadRequest,
                    "Unable to create user.");
            }

            var userId = await _unityOfWork.identityRepo.GetUserIdAsync(request.Email!);
            if (userId == null)
            {
                return Result<RegisterResponse>.Failure(
                    ResultStatus.InternalServerError,
                    "User was created but could not be retrieved.");
            }
            await _unityOfWork.identityRepo.AddToRoleAsync(userId,"Customer");

            var response=new RegisterResponse
            {
                accesstoken=_unityOfWork.JwtTokenService.GenerateAccessToken(userId,request.Email,new List<string> {"Customer" }),
                refreshtoken= await _unityOfWork.JwtTokenService.GenerateRefreshToken(userId)
            };

            return Result<RegisterResponse>.Success(response,"User registered successfully.");
        }

        public async Task<Result<RegisterResponse>> Handle(logincommand request, CancellationToken cancellationToken)
        {
            var exists = await _unityOfWork.identityRepo.UserExistsAsync(request.Email!);
            if (!exists)
            {
                return Result<RegisterResponse>.Failure(
                    ResultStatus.Unauthorized,
                    "Invalid email or password.");
            }

            var validPassword = await _unityOfWork.identityRepo.CheckPasswordAsync(request.Email!,request.Password!);

            if (!validPassword)
            {
                return Result<RegisterResponse>.Failure(
                    ResultStatus.Unauthorized,
                    "Invalid email or password.");
            }

            string userId = await _unityOfWork.identityRepo.GetUserIdAsync(request.Email!);
            var roles=await _unityOfWork.identityRepo.GetRolesAsync(userId);


            var response = new RegisterResponse
            {
                accesstoken = _unityOfWork.JwtTokenService.GenerateAccessToken(userId, request.Email,roles),
                refreshtoken = await _unityOfWork.JwtTokenService.GenerateRefreshToken(userId)
            };

            return Result<RegisterResponse>.Success(response, "Login successful.");
        }
    }
}
