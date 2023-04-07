using Azure.Core;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ProductCatalog.Application.Abstraction.Messaging;
using ProductCatalog.Application.Products.Commands.UpdateProduct;
using ProductCatalog.Controllers;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Resources.Commands.Create;
using ProductCatalog.Resources.Queries;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductCatalog.XUnitTests
{
    public class ProductControllerTest : BaseServiceTests
    {
        [Fact]
        public async void Test_GetQueryMethod()
        {
            var mockMediator = new Mock<IMediator>();
            var mockSender = new Mock<ISender>();
            var controller = new ProductsController(mockMediator.Object, mockSender.Object);

            // Act
            var result = await controller.GetProducts() as OkObjectResult;

            // Assert

            Assert.NotNull(result);
        }

         [Fact]
        public async void Test_GetAllProductByIdQueryHandler()
        {
             var handler = new GetProductByIdQueryHandler(DbContext);

            var result = await handler.Handle(new GetProductByIdQuery(1),default);

            Assert.NotNull(result);
        }

          [Fact]
        public async void Test_CreateProductCommandHandler()
        {
             var handler = new CreateProductCommandHandler(DbContext,new FakeDataStore() {  });
            var product = new Product() { Name = "dsadas", Description = "dsfsdfsf", IsActive = "true", Category = "sadsada", Price = 10.20M };
            var result = await handler.Handle(new CreateProductCommand(product), default);

            Assert.NotNull(result);
        }

         [Fact]
        public async void Test_UpdateProductCommandHandler()
        {
             var handler = new Resources.Commands.Update.UpdateProductCommandHandler(DbContext);
            var product = new Product() { Name = "dsadas", Description = "dsfsdfsf", IsActive = "true", Category = "sadsada", Price = 10.20M };
            var result = await handler.Handle(new Resources.Commands.Update.UpdateProductCommand(), default);

            Assert.Null(result);
        }
         [Fact]
          public async void Test_DeleteProductCommandHandler()
        {
             var handler = new Resources.Commands.Delete.DeleteProductCommandHandler(DbContext);
           
            var result = await handler.Handle(new Resources.Commands.Delete.DeleteProductCommand(), default);

            Assert.Null(result);
        }

         [Fact]
        public async void Test_GetAllProductsQueryHandler()
        {
             var handler = new GetAllProductsQueryHandler(DbContext);

            var result = await handler.Handle(new GetAllProductsQuery(), default);

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_UpdateProductMethod()
        {
            var mockMediator = new Mock<IMediator>();
            var mockSender = new Mock<ISender>();
            var controller = new ProductsController(mockMediator.Object, mockSender.Object);

            // Act
            var result = await controller.UpdateProduct(1, new UpdateProductRequest("name", "desc"), default);

            // Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_UpdateProductCommandHandler_Id_IsValid()
        {

            var handler = new UpdateProductCommandHandler(DbContext);

            var result = handler.Handle(new UpdateProductCommand(1, "dasdas", "asds"), default);

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_UpdateProductCommandHandler_Id_IsNotValid()
        {

            var handler = new UpdateProductCommandHandler(DbContext);

            var result = handler.Handle(new UpdateProductCommand(2, "dasdas", "asds"), default);

            Assert.NotNull(result);
        }
       
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateCommandProductValidator_Name_cannot_be_empty(string name)
        {
            // Arrange
            var updateRequest = new UpdateProductCommand(1, name, "DWESXC");
    
            var validator = new UpdateProductCommandValidator();

            // Act
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(updateRequest);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);

            // ako ima w methoda throw exception
          //  ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
		//validator.TestValidate(updateRequest).ShouldHaveValidationErrorFor(l => l.Name));
    	    //	Assert.Contains("The length of 'Nick Names' must be at least 5 characters. You entered 4 characters.", validationTestException.Message);

            //ili
            //Assert.Throws<ValidationTestException>(() => validator.TestValidate(updateRequest).ShouldHaveValidationErrorFor(x => x.Name));
         }

          [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateCommandProductValidator_Description_cannot_be_empty(string description)
        {
            // Arrange
            var updateRequest = new UpdateProductCommand(1, "name", description);
    
            var validator = new UpdateProductCommandValidator();

            // Act
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(updateRequest);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
         }

         [Theory]
        [InlineData("descrissss")]
        
        public void UpdateCommandProductValidator_Description_Value_Correct(string description)
        {
            // Arrange
            var updateRequest = new UpdateProductCommand(1, "name", description);
    
            var validator = new UpdateProductCommandValidator();

            // Act
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(updateRequest);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
         }

         [Theory]
        [InlineData("nameeeeee")]
        
        public void UpdateCommandProductValidator_Name_Value_Correct(string name)
        {
            // Arrange
            var updateRequest = new UpdateProductCommand(1, name, "dsdasdasda");
    
            var validator = new UpdateProductCommandValidator();

            // Act
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(updateRequest);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
         }

    }
}