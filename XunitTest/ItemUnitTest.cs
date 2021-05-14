using InventoryCore.Controllers;
using InventoryCore.Models;
using InventoryCore.Repository;
using InventoryCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace XunitTest
{
    public class ItemUnitTest
    {
        private RepoItem repository;
        public  DbContextOptions<ApplicationContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=DESKTOP-1QMSMIH\\SQLEXPRESS;initial catalog=InventoryCore_DB;Integrated Security=True;";

        public ItemUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationContext(dbContextOptions);
            repository = new RepoItem(context);
        }

        [Fact]
        public async void Task_GetItemId_MatchResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            int? ItemId = 1;

            //Act  
            var data = await controller.GetItem(ItemId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<ItemViewModel>().Subject;

            Assert.Equal("Tablet", post.Item_Name);
            Assert.Equal(300, post.Price);
            Assert.Equal("Updated", post.Description);
        }
        [Fact]
        public async void Task_GetItemById_Return_OkResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            var ItemId = 1;

            //Act  
            var data = await controller.GetItem(ItemId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetItemById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            var ItemId = 3;

            //Act  
            var data = await controller.GetItem(ItemId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetItems_MatchResult()
        {
            //Arrange  
            var controller = new ItemController(repository);

            //Act  
            var data = await controller.GetItems();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<List<ItemMaster>>().Subject;

            Assert.Equal("Tablet", post[0].Item_Name);
            Assert.Equal(300, post[0].Price);
            Assert.Equal("Updated", post[0].Description);          
        }

        [Fact]
        public async void Task_Add_ItemData_MatchResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            var post = new ItemViewModel() { Item_Name = "Mobile", Price = 560, Description = "test in unit test"};

            //Act  
            var data = await controller.AddItem(post);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;            

            Assert.Equal(8, okResult.Value);
        }

        [Fact]
        public async void Task_Update_ItemData_Return_OkResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            var ItemId = 4;

            //Act  
            var existingPost = await controller.GetItem(ItemId);
            var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<ItemViewModel>().Subject;

            var item = new ItemViewModel();
            item.Item_Name = "Computer";
            item.Price = 400;
            item.Description = "Last Updated by Test";
            

            var updatedData = await controller.UpdateItem(item);

            //Assert  
            Assert.IsType<OkResult>(updatedData);
        }

        [Fact]
        public async void Task_Delete_Item_Return_OkResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            var Id = 5;

            //Act  
            var data = await controller.DeleteItem(Id);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async void Task_Delete_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new ItemController(repository);
            int? ItemId = null;

            //Act  
            var data = await controller.DeleteItem(ItemId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
    }
}
