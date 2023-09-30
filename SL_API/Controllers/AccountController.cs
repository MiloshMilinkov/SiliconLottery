using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SL_API.Dtos;
using SL_API.Errors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SL_API.Extensions;
using AutoMapper;

namespace SL_API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
          private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _tokenService=tokenService;
            _mapper = mapper;

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser(){
            var user= await _userManager.FindByEmailClaimsPrincipal(User);
            return new UserDto{ 
                displayName=user.DisplayName,
                token=_tokenService.CreateToken(user),
                email=user.Email
            } ;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logInDto)
        {
            var foundUser= await _userManager.FindByEmailAsync(logInDto.Email);
            if( foundUser == null){
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(foundUser,logInDto.Password,false);

            if(!result.Succeeded){
                return Unauthorized(new ApiResponse(401));
            }
            return new UserDto{
                email=foundUser.Email,
                token = _tokenService.CreateToken(foundUser),
                displayName=foundUser.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

            if (emailExists(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult (
                    new ApiValidationErrorResponse{ 
                        Errors = new [] {"Email address already exists and is in use."} 
                        }
                    );
            }

            var user=new AppUser{
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result=await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded){
                return BadRequest(new ApiResponse(400));
            }

            return new UserDto{ 
                displayName = user.DisplayName,
                token = _tokenService.CreateToken(user),
                email = user.Email
            } ;
        }

        
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> emailExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email)!=null;
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAdress()
        {
            var user=await _userManager.FindUserByClaimsPrincipalWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address, AddressDto>(user.Address);
            
        }
    }
}