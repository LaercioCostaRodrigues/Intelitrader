using PersonRegistry.Controllers;
using PersonRegistry.Interfaces;
using PersonRegistry.Data;
using PersonRegistry.Models;
using System;
using Xunit;
//using Moq;
//using FluentAssertions;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;


using System.Linq;
using Microsoft.EntityFrameworkCore;

using FakeItEasy;


namespace UsersControllerTest
{
    public class UsersControllerTest
    {


        [Fact]
        public void GetUser_ReturnAllUsers()
        {
            // Arrange
            int count = 3;
            var fakeUsers = A.CollectionOfDummy<User>(count).AsEnumerable();
            var dataStore = A.Fake<IUserRepository>();
            var controller = new UsersController(dataStore);

            A.CallTo(() => dataStore.GetAllAsync()).Returns((fakeUsers.ToList()));

            // Act
            var actionResult = controller.GetUser();

            // Assert
            var result = actionResult.Result;
            var returnUsers = result.Value as IEnumerable<User>;
            Assert.Equal(count, returnUsers. Count());

        }

        





    }



}
