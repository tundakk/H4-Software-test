//using AutoFixture.Xunit2;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using System.Security.Claims;

//namespace H4_Software_test.Areas.Identity.Tests
//{
//    public class RevalidatingIdentityAuthenticationStateProviderTests
//    {
//        [Theory, AutoData]
//        public void ValidateAuthenticationState_WhenUserIsNull_ReturnsFalse(
//            [Frozen] Mock<UserManager<object>> userManagerMock,
//            [Frozen] Mock<IServiceScope> serviceScopeMock,
//            RevalidatingIdentityAuthenticationStateProvider<object> authStateProvider)
//        {
//            // Arrange
//            var authenticationState = new AuthenticationState(new ClaimsPrincipal());
//            userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
//                .ReturnsAsync((object)null);

//            serviceScopeMock.Setup(ss => ss.ServiceProvider)
//                .Returns(new ServiceCollection().BuildServiceProvider());

//            authStateProvider.SetScopeFactoryForTest(serviceScopeMock.Object);

//            // Act
//            bool result = authStateProvider.ValidateAuthenticationStateAsync(authenticationState,
//                default).Result;

//            // Assert
//            Assert.False(result);
//        }

//        [Theory, AutoData]
//        public void ValidateAuthenticationState_WhenUserSecurityStampNotSupported_ReturnsTrue(
//            [Frozen] Mock<UserManager<object>> userManagerMock,
//            [Frozen] Mock<IServiceScope> serviceScopeMock,
//            [Frozen] IdentityOptions options,
//            RevalidatingIdentityAuthenticationStateProvider<object> authStateProvider)
//        {
//            // Arrange
//            var authenticationState = new AuthenticationState(new ClaimsPrincipal());
//            userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
//                .ReturnsAsync(new object());

//            userManagerMock.SetupGet(um => um.SupportsUserSecurityStamp)
//                .Returns(false);

//            options.ClaimsIdentity.SecurityStampClaimType = "StampClaimType";

//            serviceScopeMock.Setup(ss => ss.ServiceProvider)
//                .Returns(new ServiceCollection().BuildServiceProvider());

//            authStateProvider.SetScopeFactoryForTest(serviceScopeMock.Object);

//            // Act
//            var result = authStateProvider.ValidateAuthenticationStateAsync(authenticationState, default).Result;

//            // Assert
//            Assert.True(result);
//        }

//        [Theory, AutoData]
//        public void ValidateAuthenticationState_WhenSecurityStampsMatch_ReturnsTrue(
//            [Frozen] Mock<UserManager<object>> userManagerMock,
//            [Frozen] Mock<IServiceScope> serviceScopeMock,
//            RevalidatingIdentityAuthenticationStateProvider<object> authStateProvider)
//        {
//            // Arrange
//            const string stampClaimType = "StampClaimType";
//            var securityStamp = "SecurityStamp";

//            var claims = new[]
//            {
//                new Claim(stampClaimType, securityStamp),
//            };

//            var user = new object();

//            var authenticationState = new AuthenticationState(new ClaimsPrincipal(claims));

//            userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
//                .ReturnsAsync(user);

//            userManagerMock.SetupGet(um => um.SupportsUserSecurityStamp)
//                .Returns(true);

//            userManagerMock.Setup(um => um.GetSecurityStampAsync(user))
//                .ReturnsAsync(securityStamp);

//            serviceScopeMock.Setup(ss => ss.ServiceProvider)
//                .Returns(new ServiceCollection().BuildServiceProvider());

//            authStateProvider.SetScopeFactoryForTest(serviceScopeMock.Object);

//            // Act
//            var result = authStateProvider.ValidateAuthenticationStateAsync(authenticationState, default).Result;

//            // Assert
//            Assert.True(result);
//        }

//        [Theory, AutoData]
//        public void ValidateAuthenticationState_WhenSecurityStampsDoNotMatch_ReturnsFalse(
//            [Frozen] Mock<UserManager<object>> userManagerMock,
//            [Frozen] Mock<IServiceScope> serviceScopeMock,
//            RevalidatingIdentityAuthenticationStateProvider<object> authStateProvider)
//        {
//            // Arrange
//            const string stampClaimType = "StampClaimType";
//            var securityStamp = "SecurityStamp";
//            var claims = new[]
//            {
//                new Claim(stampClaimType, securityStamp),
//            };

//            var authenticationState = new AuthenticationState(new ClaimsPrincipal(claims));

//            var userSecurityStamp = "NotMatchingStamp";

//            userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
//                .ReturnsAsync(new object());

//            userManagerMock.SetupGet(um => um.SupportsUserSecurityStamp)
//                .Returns(true);

//            userManagerMock.Setup(um => um.GetSecurityStampAsync(It.IsAny<object>()))
//                .ReturnsAsync(userSecurityStamp);

//            serviceScopeMock.Setup(ss => ss.ServiceProvider)
//                .Returns(new ServiceCollection().BuildServiceProvider());

//            authStateProvider.SetScopeFactoryForTest(serviceScopeMock.Object);

//            // Act
//            var result = authStateProvider.ValidateAuthenticationStateAsync(authenticationState, default).Result;

//            // Assert
//            Assert.False(result);
//        }
//    }
//}
namespace H4_Software_test.Tests
{
    using H4_Software_test.Areas.Identity.Pages.Account;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class RegisterModelTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManager;
        private readonly Mock<IUserStore<IdentityUser>> _userStore;
        private readonly Mock<IUserEmailStore<IdentityUser>> _emailStore;
        private readonly Mock<SignInManager<IdentityUser>> _signInManager;
        private readonly Mock<ILogger<RegisterModel>> _logger;
        private readonly Mock<IEmailSender> _emailSender;
        private readonly RegisterModel _registerModel;

        public RegisterModelTests()
        {
            _userStore = new Mock<IUserStore<IdentityUser>>();
            _emailStore = new Mock<IUserEmailStore<IdentityUser>>();
            _userManager = new Mock<UserManager<IdentityUser>>(_userStore.Object, null, null, null, null, null, null, null, null);
            _signInManager = new Mock<SignInManager<IdentityUser>>(_userManager.Object, null, null, null, null, null);
            _logger = new Mock<ILogger<RegisterModel>>();
            _emailSender = new Mock<IEmailSender>();
            _registerModel = new RegisterModel(_userManager.Object, _userStore.Object, _signInManager.Object, _logger.Object, _emailSender.Object);
        }

        //[Fact]
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    // Arrange
        //    string returnUrl = "returnUrl";

        //    // Act
        //    var result = await _registerModel.OnGetAsync(returnUrl);

        //    // Assert
        //    return result;
        //}


        [Fact]
        public async Task OnPostAsync_ValidModelState_ReturnsLocalRedirectResult()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var user = new IdentityUser();
            var email = "test@example.com";
            var password = "password";
            _registerModel.Input = new RegisterModel.InputModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.GetUserIdAsync(It.IsAny<IdentityUser>())).ReturnsAsync(user.Id);
            _userManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>())).ReturnsAsync("code");
            _signInManager.Setup(x => x.SignInAsync(It.IsAny<IdentityUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(0)).Verifiable();

            // Act
            var result = await _registerModel.OnPostAsync(returnUrl);

            // Assert
            var localRedirectResult = Assert.IsType<LocalRedirectResult>(result);
            Assert.Equal(returnUrl, localRedirectResult.Url);
            _signInManager.Verify();
        }

        [Fact]
        public async Task OnPostAsync_InvalidModelState_ReturnsPageResult()
        {
            // Arrange
            string returnUrl = "returnUrl";
            _registerModel.ModelState.AddModelError(string.Empty, "Test Error");

            // Act
            var result = await _registerModel.OnPostAsync(returnUrl);

            // Assert
            var pageResult = Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_UserCreationFails_AddsModelError()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var user = new IdentityUser();
            var email = "test@example.com";
            var password = "password";
            _registerModel.Input = new RegisterModel.InputModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Test Description" } }));

            // Act
            var result = await _registerModel.OnPostAsync(returnUrl);

            // Assert
            var pageResult = Assert.IsType<PageResult>(result);
            Assert.NotEmpty(_registerModel.ModelState[string.Empty].Errors);
        }

        [Fact]
        public void CreateUser_NotSupportedUserEmail_ThrowsNotSupportedException()
        {
            // Arrange
            _userManager.Setup(x => x.SupportsUserEmail).Returns(false);

            // Assert
            Assert.Throws<NotSupportedException>(() => _registerModel.CreateUser());
        }


        [Fact]
        public void CreateUser_UserEmailSupported_CreatesNewUserInstance()
        {
            // Arrange
            _userManager.Setup(x => x.SupportsUserEmail).Returns(true);

            // Act
            var user = _registerModel.CreateUser();

            // Assert
            Assert.IsType<IdentityUser>(user);
        }

        [Fact]
        public void GetEmailStore_NotSupportedUserEmail_ThrowsNotSupportedException()
        {
            // Arrange
            _userManager.Setup(x => x.SupportsUserEmail).Returns(false);

            // Assert
            Assert.Throws<NotSupportedException>(() => _registerModel.GetEmailStore());
        }

        [Fact]
        public void GetEmailStore_UserEmailSupported_ReturnsEmailStore()
        {
            // Arrange
            _userManager.Setup(x => x.SupportsUserEmail).Returns(true);

            // Act
            var emailStore = _registerModel.GetEmailStore();

            // Assert
            Assert.IsType<Mock<IUserEmailStore<IdentityUser>>>(emailStore);
        }
    }
}
