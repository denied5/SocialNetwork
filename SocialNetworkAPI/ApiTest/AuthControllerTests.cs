using api.Controllers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ApiTest
{
    public class AuthControllerTests
    {
        AuthController _controller;
        Mock<IUserService> _moqUser;
        Mock<IAuthService> _moqAuth;
        Mock<IConfiguration> configuration;

        [SetUp]
        public void SetUp()
        {
            _moqUser = new Mock<IUserService>();
            _moqAuth = new Mock<IAuthService>();
            configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("AuthKey:Token").Value)
                .Returns("fkjfndskjfndskfndskjfnkjnasknfdksafn");

            var cp = new Mock<ClaimsPrincipal>();
            cp.Setup(m => m.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, "42"));

            _controller = new AuthController(_moqAuth.Object, _moqUser.Object, configuration.Object);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = cp.Object;
        }

        

    }
}
