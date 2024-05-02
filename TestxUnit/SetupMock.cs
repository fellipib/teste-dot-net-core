using dotnet_teste.Controllers;
using dotnet_teste.Models;
using dotnet_teste.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TestxUnit
{
    public class SetupMock
    {

                     



        public static void CreateMockData(ApplicationDbContext context)
        {
            

            context.Books.Add(new Books() { Id = 1, NSU = 1, Description = "teste1", Writer = "testador", Category = "xUnit", Title = "teste1" });
            context.Books.Add(new Books() { Id = 2, NSU = 2, Description = "teste2", Writer = "testador", Category = "xUnit", Title = "teste2" });
            context.Books.Add(new Books() { Id = 3, NSU = 3, Description = "teste3", Writer = "testador", Category = "xUnit", Title = "teste3" });
            context.Books.Add(new Books() { Id = 4, NSU = 4, Description = "teste4", Writer = "testador", Category = "xUnit", Title = "teste4" });
            context.Books.Add(new Books() { Id = 5, NSU = 5, Description = "teste5", Writer = "testador", Category = "xUnit", Title = "teste5" });
            context.Books.Add(new Books() { Id = 6, NSU = 6, Description = "teste6", Writer = "testador", Category = "xUnit", Title = "teste6" });
            context.Books.Add(new Books() { Id = 7, NSU = 7, Description = "teste7", Writer = "testador", Category = "xUnit", Title = "teste7" });
            context.Books.Add(new Books() { Id = 8, NSU = 8, Description = "teste8", Writer = "testador", Category = "xUnit", Title = "teste8" });

            context.Loans.Add(new Loan() { Id = 1, BookNSU = 1, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744", ExpectedDelivery = DateTime.Today.AddDays(-1), IsDelivered = false});
            context.Loans.Add(new Loan() { Id = 2, BookNSU = 2 , UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 3, BookNSU = 2 , UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 4,BookNSU = 3 , UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 5,BookNSU = 3, UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 6,BookNSU = 3 , UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 7,BookNSU = 4, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 8,BookNSU = 4, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 9,BookNSU = 4, UserId = "513d5053-f8a4-4870-b730-139ebe9e2027"});
            context.Loans.Add(new Loan() { Id = 10,BookNSU = 4, UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 11, BookNSU = 5, UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 12, BookNSU = 5, UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 13, BookNSU = 5, UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 14, BookNSU = 5, UserId = "a7a9da33-d212-496b-ad2d-41b2ae8a83d0" });
            context.Loans.Add(new Loan() { Id = 15, BookNSU = 5 , UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 17, BookNSU = 5 , UserId = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.Loans.Add(new Loan() { Id = 18, BookNSU = 6, UserId = "a7a9da33-d212-496b-ad2d-41b2ae8a83d0" });
            context.Loans.Add(new Loan() { Id = 19, BookNSU = 6 , UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 20, BookNSU = 7 , UserId = "a7a9da33-d212-496b-ad2d-41b2ae8a83d0" });
            context.Loans.Add(new Loan() { Id = 21, BookNSU = 8 , UserId = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.Loans.Add(new Loan() { Id = 22, BookNSU = 8 , UserId = "a7a9da33-d212-496b-ad2d-41b2ae8a83d0" });
            context.Loans.Add(new Loan() { Id = 23, BookNSU = 8 , UserId = "a7a9da33-d212-496b-ad2d-41b2ae8a83d0" });

            context.aspNetUsers.Add(new IdentityUser { UserName = "some name", Id = "847e3c05-8e22-4c8b-9741-290ec70c6744" });
            context.aspNetUsers.Add(new IdentityUser { UserName = "some name2", Id = "513d5053-f8a4-4870-b730-139ebe9e2027" });
            context.aspNetUsers.Add(new IdentityUser { UserName = "some name3", Id = "a7a9da33-d212-496b-ad2d-41b2ae8a83d0" });

            context.SaveChanges();
        }
}
}
