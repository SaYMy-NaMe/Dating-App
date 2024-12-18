using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")] //account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) //Using DTO to check the body of the request 
    //Sending Method: HTTP Headers, Body of the HTTP request, Query String Parametres (Could be any of these) | [FromQuery]/[FromBody]
    {
        if(await UserExists(registerDto.Username)) return BadRequest("UserName has already taken :3"); 

        using var hmac = new HMACSHA512(); 
        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(), 
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), 
            PasswordSalt = hmac.Key
        }; 
        
        context.Users.Add(user); 
        await context.SaveChangesAsync(); 
        
        return user; 
    }
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower( ) == username.ToLower());  //lamda Expression
    }
} 
