using api.Controllers;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiTest
{
    public class UserConntrollerTests
    {
        Mock<IUserService> _moq;
        UsersController _controller;

        List<UserForListDTO> _users = new List<UserForListDTO>()
            {
                new UserForListDTO { Id = 1, KnownAs = "David"},
                new UserForListDTO { Id = 2, KnownAs = "Entony"},
                new UserForListDTO { Id = 3, KnownAs = "Cris"},
                new UserForListDTO { Id = 42, KnownAs = "Denys"},
            };
        UserForDetailedDTO _user = new UserForDetailedDTO { Id = 42, KnownAs = "Denys" };
        UserForDetailedDTO _nullUser;
        PagedList<UserForListDTO> _usersPaged;
        UserParams _userParams = new UserParams();
        UserParams _userNullParams = new UserParams();
        UserForUpdateDTO _userForUpdateDTO = new UserForUpdateDTO() { };
        UserForUpdateDTO _userForFailUpdateDTO = new UserForUpdateDTO() { };

        [SetUp]
        public void Setup()
        {
            _usersPaged = PagedList<UserForListDTO>.Create(_users, 1, 10);

            _moq = new Mock<IUserService>();
            _moq.Setup((x) => x.GetUser(42, true))
                .Returns(Task.FromResult(_user));
            _moq.Setup((x) => x.GetUser(-1, false))
                .Returns(Task.FromResult(_nullUser));
            _moq.Setup((x) => x.GetUsers(_userParams))
                .Returns(Task.FromResult(PagedList<UserForListDTO>.Create(_usersPaged,
                1, 10)));
            _moq.Setup((x) => x.GetUsers(_userNullParams));
            _moq.Setup(x => x.UpdateUser(42, _userForUpdateDTO))
                .Returns(Task.FromResult(true));
            _moq.Setup(x => x.UpdateUser(42, _userForFailUpdateDTO))
                .Returns(Task.FromResult(false));

            var cp = new Mock<ClaimsPrincipal>();
            cp.Setup(m => m.HasClaim(It.IsAny<string>(), It.IsAny<string>()))
              .Returns(true);
            cp.Setup(m => m.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, "42"));

            _controller = new UsersController(_moq.Object);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = cp.Object;
        }

        [Test]
        [Category("pass")]
        public async Task GetUserTest()
        {
            var result = await _controller.GetUser(42);
            Assert.IsNotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var user = okObjectResult.Value as UserForDetailedDTO;
            Assert.NotNull(user);
            Assert.AreEqual(42, user.Id);
        }

        [Test]
        [Category("pass")]
        public async Task GetUsersTest()
        {
            var result = await _controller.GetUsers(_userParams);
            Assert.IsNotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var users = okObjectResult.Value as List<UserForListDTO>;
            Assert.NotNull(users);
            Assert.AreEqual(42, users[3].Id);
            Assert.AreEqual("Denys", users[3].KnownAs);
        }

        [Test]
        [Category("fail")]
        public async Task GetUserFailTest()
        {
            var result = await _controller.GetUser(-1);
            Assert.IsNotNull(result);

            var okObjectResult = result as OkObjectResult;
            var BadRequestResult = result as BadRequestObjectResult;
            Assert.IsNull(okObjectResult);
            Assert.NotNull(BadRequestResult);
        }

        [Test]
        [Category("fail")]
        public async Task GetUsersFailTest()
        {
            var result = await _controller.GetUsers(_userNullParams);
            Assert.IsNotNull(result);

            var okObjectResult = result as OkObjectResult;
            var BadRequestResult = result as BadRequestObjectResult;
            Assert.IsNull(okObjectResult);
            Assert.NotNull(BadRequestResult);
        }

        [Test]
        [Category("pass")]
        public async Task UpdateUserTest()
        {
            var result = await _controller.UpdateUser(42, _userForUpdateDTO);
            Assert.IsNotNull(result);

            var NoContentObjectResult = result as NoContentResult;
            Assert.IsNotNull(NoContentObjectResult);
        }

        [Test]
        [Category("fail")]
        public async Task UpdateUser_UnauthorizeFailTest()
        {
            var result = await _controller.UpdateUser(2, _userForUpdateDTO);
            Assert.IsNotNull(result);
            
            var unauthorizedObjectResult = result as Microsoft.AspNetCore.Mvc.UnauthorizedResult;
            Assert.IsNotNull(unauthorizedObjectResult);
        }

        [Test]
        [Category("fail")]
        public async Task UpdateUser_SaveFailTest()
        {
            var result = await _controller.UpdateUser(42, _userForFailUpdateDTO);
            Assert.IsNotNull(result);

            var BadRequestObjectResult = result as BadRequestObjectResult;
            Assert.IsNotNull(BadRequestObjectResult);
        }
    }
}