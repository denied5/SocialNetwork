using api.Controllers;
using BIL.DTO;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{
    public class AuthControllerTests
    {
        AuthController _controller;
        Mock<IUserService> _moqUser;
        Mock<IAuthService> _moqAuth;
        Mock<IConfiguration> configuration;

        UserForRegisterDTO userForRegisterDTOSuccess = new UserForRegisterDTO() { Username = "Denys"};
        UserForDetailedDTO userForDetailedDTOSuccess = new UserForDetailedDTO();
        UserForRegisterDTO userForRegisterDTOFail = new UserForRegisterDTO();
        UserForDetailedDTO userForDetailedDTOFail = null;
        UserForLoginDTO userForLoginDTOSuccess = new UserForLoginDTO();
        UserForListDTO userForListDTOSuccess = new UserForListDTO();
        UserForLoginDTO userForLoginDTOFail = new UserForLoginDTO();
        UserForListDTO userForListDTOFail = null;


        [SetUp]
        public void SetUp()
        {
            _moqAuth = new Mock<IAuthService>();

            _moqAuth.Setup(x => x.Register(userForRegisterDTOSuccess))
                .Returns(Task.FromResult(userForDetailedDTOSuccess));
            _moqAuth.Setup(x => x.Register(userForRegisterDTOFail))
                .Returns(Task.FromResult(userForDetailedDTOFail));
            _moqAuth.Setup(x => x.LogIn(userForLoginDTOSuccess))
                .Returns(Task.FromResult(userForListDTOSuccess));
            _moqAuth.Setup(x => x.LogIn(userForLoginDTOFail))
                .Returns(Task.FromResult(userForListDTOFail));



            configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("AuthKey:Token").Value)
                .Returns("fkjfndskjfndskfndskjfnkjnasknfdksafn");
            LogFactory logFactory = new LogFactory();
            logFactory.Configuration = new NLog.Config.LoggingConfiguration();
            //TODO: How to add loger in Controoller?
            ILogger<AuthController> logger = logFactory.GetCurrentClassLogger(typeof(AuthController)) as ILogger<AuthController>;

            var cp = new Mock<ClaimsPrincipal>();
            cp.Setup(m => m.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, "42"));
            _controller = new AuthController(_moqAuth.Object, configuration.Object, logger);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = cp.Object;
        }

        [Test]
        [Category("pass")]
        public async Task Reggister_ValidUser_CreatedAtRoute()
        {
            //arrange

            //act
            var result = await _controller.Register(userForRegisterDTOSuccess);
            var createdAtRoute = result as CreatedAtRouteResult;
            var routeValueId = createdAtRoute.RouteValues["id"];
            var postForReturn = createdAtRoute.Value as PostForReturnDTO;

            //assert
            Assert.AreEqual(postForReturn.Id, routeValueId);
            Assert.IsNotNull(result);
            Assert.IsNotNull(createdAtRoute);
            Assert.IsNotNull(postForReturn);
        }



    }
}
