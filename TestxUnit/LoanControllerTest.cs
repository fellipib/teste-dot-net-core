using dotnet_teste.Controllers;
using dotnet_teste.Models;
using dotnet_teste.Properties;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;



namespace TestxUnit
{
    public class LoanControllerTest
    {
        private ServiceProvider _serviceProvider;

                
        public LoanControllerTest()
        {
            _serviceProvider = CreateRepository();
            var context = _serviceProvider.GetService<ApplicationDbContext>();
            SetupMock.CreateMockData(context);
        }


        public static Mock<UserManager<IdentityUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var mgr = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<IdentityUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<IdentityUser>());
            mgr.Setup(x => x.FindByNameAsync("some name")).Returns(Task.FromResult(new IdentityUser { UserName = "some name", Id = "847e3c05-8e22-4c8b-9741-290ec70c6744" }));
            return mgr;
        }

        private ServiceProvider CreateRepository()
        {
            
            var auth = MockUserManager();
            var httpContext = new DefaultHttpContext();
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(x => { x.UseInMemoryDatabase(databaseName: "StoreMemoryContext", new InMemoryDatabaseRoot()); });
            services.AddSingleton(auth.Object);
            services.AddSingleton<HttpContext>(httpContext);
            services.AddScoped<BooksController>();
            services.AddScoped<LoanController>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        [Fact]
        public async Task GetAllLoans_Sucess()
        {
            //arrange            
            var controller = _serviceProvider.GetService<LoanController>();

            // Act
            var res = await controller.GetAll();
            var resType = res as OkObjectResult;
            var resList = resType.Value as List<Loan>;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<List<Loan>>(resType.Value);
            Assert.Equal(22, resList.Count);

        }
        [Fact]
        public async Task GetByNsu_Sucess()
        {
            //arrange           
            var controller = _serviceProvider.GetService<LoanController>();
            var testeLoan = new List<Loan>()
            {
                new Loan() { BookNSU = 1, Id = 1, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744", ExpectedDelivery = DateTime.Today.AddDays(-1), IsDelivered = false}
            };
            var testeJson = JsonConvert.SerializeObject(testeLoan);

                        // Act
            var res = await controller.LoansByBookNSU(1);
            var resType = res as OkObjectResult;
            var resList = resType.Value as List<Loan>;

            //colocando usuarios como null pra nao ter que mockar todos os usuarios de um emprestimo
            resList.ForEach(x => x.User = null);
            var resJson = JsonConvert.SerializeObject(resList);

            // Assert
            Assert.NotNull(res);
            Assert.IsType<List<Loan>>(resType.Value);
            Assert.Equivalent(testeJson, resJson);

        }

        [Fact]
        public async Task Post_Sucess()
        {
            //arrange
            var context = _serviceProvider.GetService<ApplicationDbContext>();
            var controller = _serviceProvider.GetService<LoanController>();
            var identity = new GenericIdentity("some name", "test");
            var contextUser = new ClaimsPrincipal(identity);            

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = contextUser},
                
               
            };
            
            
                
            var testeLoan = new Loan() { Id = 24, BookNSU= 4, BookId = 4 };

            // Act
            var res = await controller.Register(testeLoan);
            var resType = res as OkObjectResult;
            var resList = resType.Value as Loan;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<Loan>(resType.Value);
            Assert.Equivalent(testeLoan, resList);

        }
        [Fact]
        public async Task Put_Sucess()
        {
            //arrange

            var controller = _serviceProvider.GetService<LoanController>();
            var identity = new GenericIdentity("some name", "test");
            var contextUser = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = contextUser },
            };
            var testeLoan = new Loan() { Id = 24, BookNSU = 4, BookId = 4, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" };

            // Act
            var res = await controller.Delivery(testeLoan);
            var resType = res as OkObjectResult;
            var resList = resType.Value as string;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<string>(resType.Value);
            Assert.Equal("Livro devolvido", resList);

        }

        [Fact]
        public async Task GetLateDelivery_Sucess()
        {
            //arrange           
            var controller = _serviceProvider.GetService<LoanController>();
            var testeLoan = new List<Loan>()
            { new Loan {Id = 1, BookNSU = 1, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744", ExpectedDelivery = DateTime.Today.AddDays(-1), IsDelivered = false } };
            var testeJson = JsonConvert.SerializeObject(testeLoan);

            // Act
            var res = await controller.LateDeliery();
            var resType = res as OkObjectResult;
            var resList = resType.Value as List<Loan>;

            //colocando usuarios como null pra nao ter que mockar todos os usuarios de um emprestimo
            resList.ForEach(x => x.User = null);

            var resJson = JsonConvert.SerializeObject(resList);

            // Assert
            Assert.NotNull(res);
            Assert.IsType<List<Loan>>(resType.Value);
            Assert.Equivalent(testeJson, resJson);

        }

    }
}