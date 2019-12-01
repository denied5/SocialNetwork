using api.Controllers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{
    class PostsControllerTests
    {
        Mock<IPostService> _moqPost;
        Mock<IUserService> _moqUser;
        PostsController _controller;

        [SetUp]
        public void SetUp()
        {
            _moqUser = new Mock<IUserService>();
            _moqPost = new Mock<IPostService>();

            _moqPost.Setup(x => x.DeletePost(2))
                .Returns(Task.FromResult(true));
            _moqPost.Setup(x => x.DeletePost(-2))
                .Returns(Task.FromResult(false));

            var cp = new Mock<ClaimsPrincipal>();
            cp.Setup(m => m.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, "42"));

            _controller = new PostsController(_moqPost.Object, _moqUser.Object);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = cp.Object;
        }

        [Test]
        [Category("pass")]
        public async Task DeletePost_42and2_NoContent()
        {
            //arrange
            var userId = 42;
            var postId = 2;

            //act
            var result = await _controller.DeletePost(userId, postId);
            var NoContentObjectResult = result as NoContentResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(NoContentObjectResult);
        }

        [Test]
        [Category("fail")]
        public async Task DeletePost_2and2_Unauthorized()
        {
            //arrange
            var userId = 2;
            var postId = 2;

            //act
            var result = await _controller.DeletePost(userId, postId);
            var unauthorizedResult = result as UnauthorizedResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(unauthorizedResult);
        }

        [Test]
        [Category("fail")]
        public async Task DeletePost_42andMin2_BadRequest()
        {
            //arrange
            var userId = 42;
            var postId = -2;

            //act
            var result = await _controller.DeletePost(userId, postId);
            var badRequestObjectResult = result as BadRequestObjectResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(badRequestObjectResult);
        }

        [Test]
        [Category("pass")]
        public async Task DeletePost_42and2_NoContent()
        {
            //arrange
            var userId = 42;
            var postId = 2;

            //act
            var result = await _controller.DeletePost(userId, postId);
            var NoContentObjectResult = result as NoContentResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(NoContentObjectResult);
        }
    }
}
