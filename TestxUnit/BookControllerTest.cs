using dotnet_teste.Controllers;
using dotnet_teste.Models;
using dotnet_teste.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;



namespace TestxUnit
{
    public class BooksControllerTest
    {
        public ServiceProvider _serviceProvider;
        public BooksControllerTest()
        {
            _serviceProvider = CreateRepository();
            var context = _serviceProvider.GetService<ApplicationDbContext>();
            SetupMock.CreateMockData(context);
        }

        private ServiceProvider CreateRepository()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(x => { x.UseInMemoryDatabase(databaseName: "StoreMemoryContext", new InMemoryDatabaseRoot()); });
            services.AddScoped<BooksController>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        [Fact]
        public async Task GetAllBooks_Sucess()
        {
            //arrange            
            var controller = _serviceProvider.GetService<BooksController>();

            // Act
            var res = await controller.GetAll();
            var resType = res as OkObjectResult;
            var resList = resType.Value as List<Books>;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<List<Books>>(resType.Value);
            Assert.Equal(8, resList.Count);

        }
        [Fact]
        public async Task GetByNsu_Sucess()
        {
            //arrange           
            var controller = _serviceProvider.GetService<BooksController>();
            Books testeBook = new Books() { Id = 1, NSU = 1, Description = "teste1", Writer = "testador", Category = "xUnit", Title = "teste1" };


            // Act
            var res = await controller.Get(1);
            var resType = res as OkObjectResult;
            var resList = resType.Value as Books;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<Books>(resType.Value);
            Assert.Equivalent(testeBook, resList);

        }

        [Fact]
        public async Task Post_Sucess()
        {
            //arrange
            var context = _serviceProvider.GetService<ApplicationDbContext>();
            var controller = _serviceProvider.GetService<BooksController>();
            Books testeBook = new Books() { Id = 9, NSU = 9, Description = "teste9", Writer = "testador", Category = "xUnit", Title = "teste9" };

            // Act
            var res = await controller.Register(testeBook);
            var resType = res as OkObjectResult;
            var resList = resType.Value as Books;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<Books>(resType.Value);
            Assert.Equivalent(testeBook, resList);

        }
        [Fact]
        public async Task Put_Sucess()
        {
            //arrange
            var context = _serviceProvider.GetService<ApplicationDbContext>();
            var controller = _serviceProvider.GetService<BooksController>();
            Books testeBook = new Books() { Id = 1, NSU = 1, Description = "testeUpdate", Writer = "testador", Category = "xUnit", Title = "teste9" };

            // Act
            var res = await controller.Update(testeBook);
            var resType = res as OkObjectResult;
            var resList = resType.Value as Books;

            // Assert
            Assert.NotNull(res);
            Assert.IsType<Books>(resType.Value);
            Assert.Equivalent(testeBook, resList);

        }

        [Fact]
        public async Task GetMostLoaned_Sucess()
        {
            //arrange           
            var controller = _serviceProvider.GetService<BooksController>();

            var livro = new Books() { Id = 8, NSU = 8, Description = "teste8", Writer = "testador", Category = "xUnit", Title = "teste8" };
            var obj = new { totalImprestimos = 4, livro };


            // Act
            var res = await controller.getMostLoanedBooks();
            var resType = res as OkObjectResult;
            var resList = resType.Value;

            // Assert
            Assert.NotNull(res);
            //como a resposta é uma query sem um model definido esse teste não tem como passar
            //Assert.IsType<>(resType.Value);
            Assert.Equivalent(obj, resList);

        }

    }
}