using api.Controllers;
using BIL.DTO;
using BIL.Helpers;
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
        PostForCreatinDTO postForCreatin = new PostForCreatinDTO() { };
        PostForCreatinDTO PostForCreatinFail = new PostForCreatinDTO() { };
        PostForReturnDTO postForReturnDTO = new PostForReturnDTO() { };
        PagedListParams pagedListParams = new PagedListParams() { };
        PagedListParams pagedListParamsFail = new PagedListParams() { };
        PagedList<PostForReturnDTO> pagedListPost;


        [SetUp]
        public void SetUp()
        {
            pagedListPost = new PagedList<PostForReturnDTO>(
                new List<PostForReturnDTO> {
                new PostForReturnDTO {Content = "first", Id = 1},
                new PostForReturnDTO {Content = "second", Id =2}
            }, 2, pagedListParams.CurrentPage, pagedListParams.PageSize);
            

            _moqUser = new Mock<IUserService>();
            _moqPost = new Mock<IPostService>();

            _moqPost.Setup(x => x.DeletePost(2))
                .Returns(Task.FromResult(true));
            _moqPost.Setup(x => x.DeletePost(-2))
                .Returns(Task.FromResult(false));
            _moqPost.Setup(x => x.CreatePost(postForCreatin))
                .Returns(Task.FromResult(postForReturnDTO));
            _moqPost.Setup(x => x.GetFeed(pagedListParams))
                .Returns(Task.FromResult(pagedListPost));
            

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
        public async Task CreatePost_42andPost_CreatedAtRoute()
        {
            //arrange
            var userId = 42;
            var post = postForCreatin;

            //act
            var result = await _controller.CreatePost(userId, post);
            var createdAtRoute = result as CreatedAtRouteResult;
            var routeValueId = createdAtRoute.RouteValues["id"];
            var postForReturn = createdAtRoute.Value as PostForReturnDTO;
            //assert
            Assert.AreEqual(postForReturn.Id, routeValueId);
            Assert.IsNotNull(result);
            Assert.IsNotNull(createdAtRoute);
            Assert.IsNotNull(postForReturn);
        }

        [Test]
        [Category("fail")]
        public async Task CreatePost_43andPost_UnauthorizedResult()
        {
            //arrange
            var userId = 42 + 1;
            var post = postForCreatin;

            //act
            var result = await _controller.CreatePost(userId, post);
            var unauthorizedResult = result as UnauthorizedResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(unauthorizedResult);
        }

        [Test]
        [Category("fail")]
        public async Task CreatePost_42andPost_BadRequest()
        {
            //arrange
            var userId = 42;
            var post = PostForCreatinFail;

            //act
            var result = await _controller.CreatePost(userId, post);
            var badRequestResult = result as BadRequestObjectResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(badRequestResult);
        }

        [Test]
        [Category("pass")]
        public async Task GetFeed_42andParams_Ok()
        {
            //arrange
            var userId = 42;
            var param = pagedListParams;

            //act
            var result = await _controller.GetFeed(userId, param);
            var okObjectRes = result as OkObjectResult;
            var curPostsForReturn = okObjectRes.Value as List<PostForReturnDTO>;
            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(okObjectRes);
            Assert.IsNotNull(curPostsForReturn);
            Assert.AreEqual(curPostsForReturn[0].Id, pagedListPost[0].Id);
        }

        [Test]
        [Category("fail")]
        public async Task GetFeed_43andParams_Unauthorized()
        {
            //arrange
            var userId = 42 + 1;
            var param = pagedListParams;

            //act
            var result = await _controller.GetFeed(userId, param);
            var unauthorizedResult = result as UnauthorizedResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(unauthorizedResult);
        }

        [Test]
        [Category("fail")]
        public async Task GetFeed_42andParams_NoContent()
        {
            //arrange
            var userId = 42;
            var param = pagedListParamsFail;

            //act
            var result = await _controller.GetFeed(userId, param);
            var NoContentObjectResult = result as NoContentResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(NoContentObjectResult);
        }

        [Test]
        [Category("fail")]
        public async Task GetFeed_42andNull_BadRequest()
        {
            //arrange
            var userId = 42;
            PagedListParams param = null;

            //act
            var result = await _controller.GetFeed(userId, param);
            var NoContentObjectResult = result as BadRequestResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(NoContentObjectResult);
        }
    }
}
