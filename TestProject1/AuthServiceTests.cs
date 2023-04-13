using AutoMapper;
using CompanyName.Application.Dal.Auth.Configurations;
using CompanyName.Application.Dal.Auth.Repository;
using CompanyName.Application.Services.AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CompanyName.Application.Services.AuthService.Tests
{
    public class AuthServiceTests
    {
        private Mock<UserManager<IdentityUser>> userManager;
        private Mock<IAuthRepository> authRepository;
        private Mock<IJwtConfigurationSettings> jwtConfigurationSettings;
        private Mock<IMapper> autoMapper;

        [SetUp]
        public void Setup()
        {
            userManager = MockUserManager<IdentityUser>(new List<IdentityUser>());
            authRepository = new Mock<IAuthRepository>();
            jwtConfigurationSettings = new Mock<IJwtConfigurationSettings>();
            autoMapper = new Mock<IMapper>();
        }

        [Test]
        public void LoginUserTest()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                Password = "password",
                UserName = "Username"
            };

            jwtConfigurationSettings.Setup(config => config.Key).Returns("KeyStubKeyStubKeyStubKeyStubKeyStubKeyStub");
            jwtConfigurationSettings.Setup(config => config.TokenTimeToLiveMinutes).Returns(30);

            var identityUserMock = new Mock<IdentityUser>();
            identityUserMock.Setup(user => user.Email).Returns("usermail@email.com");

            userManager.Setup(m => m.FindByNameAsync(userLogin.UserName)).ReturnsAsync(identityUserMock.Object);
            userManager.Setup(m => m.CheckPasswordAsync(identityUserMock.Object, userLogin.Password)).ReturnsAsync(true);

            var authService = new Services.AuthService(
                userManager.Object,
                authRepository.Object,
                jwtConfigurationSettings.Object,
                autoMapper.Object);

            // Act
            var actual = authService.LoginUser(userLogin).Result;

            // Assert 
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual.Token);
        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}