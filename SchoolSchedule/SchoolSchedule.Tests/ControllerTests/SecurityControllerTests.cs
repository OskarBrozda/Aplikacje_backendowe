using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolSchedule.API.Controllers;
using SchoolSchedule.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Domain.Model;
using WebApi.Repository;

namespace SchoolSchedule.Tests
{
    public class SecurityControllerTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IOptions<JwtConfig>> _mockConfig;
        private readonly SecurityController _controller;

        public SecurityControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockConfig = new Mock<IOptions<JwtConfig>>();
            _controller = new SecurityController(_mockConfig.Object, _mockRepo.Object);

            _mockConfig.Setup(config => config.Value).Returns(new JwtConfig
            {
                Issuer = "admin",
                Audience = "admin",
                Key = "TurboStrongKeyValueMuchMoreThan256Bits"
            });
        }

        [Fact]
        public void GenerateToken_ReturnsOkResult_WithValidToken_WhenUserIsAuthorized()
        {
            // Arrange
            var user = new WebApiUser { UserName = "testuser", Password = "zaq12wsx" };
            _mockRepo.Setup(repo => repo.AuthorizeUser(user.UserName, user.Password)).Returns(true);

            // Act
            var result = _controller.GenerateToken(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var token = Assert.IsType<string>(okResult.Value);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_mockConfig.Object.Value.Key);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _mockConfig.Object.Value.Issuer,
                ValidAudience = _mockConfig.Object.Value.Audience
            }, out SecurityToken validatedToken);

            Assert.NotNull(validatedToken);
        }

        [Fact]
        public void GenerateToken_ReturnsUnauthorized_WhenUserIsNotAuthorized()
        {
            // Arrange
            var user = new WebApiUser { UserName = "testuser", Password = "wrongpassword" };
            _mockRepo.Setup(repo => repo.AuthorizeUser(user.UserName, user.Password)).Returns(false);

            // Act
            var result = _controller.GenerateToken(user);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
