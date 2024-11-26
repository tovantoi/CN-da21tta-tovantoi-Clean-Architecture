﻿using AutoMapper;
using chuyennganh.Application.App.CustomersApp.Customers.Command;
using chuyennganh.Application.App.CustomersApp.Customers.Query.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chuyennganh.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomersController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/register-customer")]
        public static async Task<IResult> RegisterCustomer([FromBody] RegisterRequest request, IMediator mediator)
        {

            var results = await mediator.Send(request);
            if (results.IsSuccess)
            {
                return TypedResults.Ok(results);
            }
            return TypedResults.BadRequest(results);
        }

        [HttpPost("/login-customer")]
        public static async Task<IResult> Login([FromBody] LoginRequest request, IMediator mediator)
        {
            var response = await mediator.Send(request);

            if (!response.IsSuccess)
            {
                return TypedResults.Ok(response);
            }
            return TypedResults.BadRequest(response);
        }


        [HttpPut("/authen-customer")]
        public static async Task<IResult> AuthenCustomer(string? email, [FromBody] AuthenCustomerRequest request, IMediator mediator, IMapper mapper)
        {
            var command = mapper.Map<AuthenCustomerRequest>(request);
            command.Email = email;
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpPost("/resend-otp")]
        public static async Task<IResult> ResendOTP(string? email, [FromBody] ResendOTPRequest request, IMediator mediator, IMapper mapper)
        {
            var command = mapper.Map<ResendOTPRequest>(request);
            command.Email = email;
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpPost("/customer-logout")]
        public static async Task<IResult> Logout(LogoutCustomerRequest request, IMediator mediator, IMapper mapper)
        {
            var command = mapper.Map<LogoutCustomerRequest>(request);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpPut("/update-profile-customer")]
        public static async Task<IResult> UpdateCustomerProfile(int? id, [FromBody] UpdateProifleCustomerRequest request, IMediator mediator, IMapper mapper)
        {

            var command = mapper.Map<UpdateProifleCustomerRequest>(request);
            command.Id = id;
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpGet("/get-customer-by-id")]
        public static async Task<IResult> GetCustomerById(int id, IMediator mediator)
        {
            var command = new GetCustomerByIdCustomerRequest();
            command.Id = id;
            var result = await mediator.Send(command);
            return TypedResults.BadRequest(result);
        }

        [HttpGet("/get-customers")]
        public static async Task<IResult> GetAllCustomerRoles(IMediator mediator)
        {
            var command = new GetAllCustomerRequest();
            var result = await mediator.Send(command);
            return TypedResults.BadRequest(result);
        }
    }
}

