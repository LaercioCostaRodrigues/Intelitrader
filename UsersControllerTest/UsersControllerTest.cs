using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.Controllers;
using PersonRegistry.Interfaces;
using PersonRegistry.Models;
using System.Linq;
using Xunit;


namespace UsersControllerTest
{
    public class UsersControllerTest
    {
         
        [Fact]
        public void GetUsers_ReturnAllUsers()
        {
            // Arrange
            int count = 3;
            var fakeUsers = A.CollectionOfFake<User>(count).AsEnumerable();
            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);
            A.CallTo(() => fakeRepository.GetAll()).Returns(fakeUsers.ToList());

            // Act
            var actionResult = UserController.GetUsers();

            // Assert
            Assert.Equal(count, actionResult.Value.ToList().Count);
        }
        
        [Fact]
        public void GetUser_UserById_ReturnUser()
        {
            // Arrange
            string id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            var fakeUser = A.Fake<User>();
            fakeUser.Id = id;
            fakeUser.FirstName = "Dennis";
            fakeUser.Surname = "Ritchie";
            fakeUser.Age = 70;

            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);
            A.CallTo(() => fakeRepository.Add(fakeUser));

            // Act
            var actionResult = UserController.GetUser(id);

            // Assert
            Assert.Equal(id, fakeUser.Id);
            Assert.Equal("Dennis", fakeUser.FirstName);
            Assert.Equal("Ritchie", fakeUser.Surname);
            Assert.Equal(70, fakeUser.Age);
        }      

        [Fact]
        public void GetUser_NullUser_ReturnNotFound()
        {
            // Arrange
            var fakeUser = A.Fake<User>();
            fakeUser.Id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            fakeUser.FirstName = "Anders";
            fakeUser.Surname = "Hejlsberg";
            fakeUser.Age = 61;

            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);
            A.CallTo(() => fakeRepository.Add(fakeUser));

            // Act
            var actionResult = UserController.GetUser("NonexistentId");

            // Assert
            var result = actionResult.Result;
            Assert.Null(result);
        }

        [Fact]
        public void PutUser_WithWrongId_ReturnBadRequest()
        {
            // Arrange
            string id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            var fakeUser = A.Fake<User>();
            fakeUser.Id = "NonexistentId";
            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);

            // Act
            var iactionResult = UserController.PutUser(id, fakeUser);

            // Assert
            Assert.IsType<BadRequestResult>(iactionResult);
        }

        [Fact]
        public void PutUser_WithExistentId_ReturnNoContent()
        {
            // Arrange
            string id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            var fakeUser_1 = A.Fake<User>();
            var fakeUser_2 = A.Fake<User>();

            fakeUser_1.Id = id;
            fakeUser_1.FirstName = "Dennis";
            fakeUser_1.Surname = "Ritchie";
            fakeUser_1.Age = 70;

            fakeUser_2.Id = id;
            fakeUser_2.FirstName = "Anders";
            fakeUser_2.Surname = "Hejlsberg";
            fakeUser_2.Age = 70;

            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);
            A.CallTo(() => fakeRepository.Add(fakeUser_1));


            // Act
            var iactionResult = UserController.PutUser(id, fakeUser_2);

            // Assert
            Assert.Equal(fakeUser_1.Id, fakeUser_2.Id);
            Assert.NotEqual(fakeUser_1, fakeUser_2);
            Assert.NotEqual(fakeUser_1.FirstName, fakeUser_2.FirstName);
            Assert.NotEqual(fakeUser_1.Surname, fakeUser_2.Surname);
            Assert.Equal(fakeUser_1.Age, fakeUser_2.Age);
            Assert.IsType<NoContentResult>(iactionResult);
        }

        [Fact]
        public void PostUser_RightRequestBody_UserActionResult()
        {
            // Arrange
            var fakeUser = A.Fake<User>();
            fakeUser.Id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            fakeUser.FirstName = "Anders";
            fakeUser.Surname = "Hejlsberg";
            fakeUser.Age = 61;

            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);

            // Act
            var actionResult = UserController.PostUser(fakeUser);

            // Assert
            Assert.IsType<ActionResult<User>>(actionResult);
        }

        [Fact]
        public void DeleteUser_WithNoexistentUser_ReturnNotFound()
        {
            // Arrange
            var fakeUser = A.Fake<User>();
            fakeUser.Id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            fakeUser.FirstName = "Anders";
            fakeUser.Surname = "Hejlsberg";
            fakeUser.Age = 61;

            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);
            A.CallTo(() => fakeRepository.Add(fakeUser));
            A.CallTo(() => fakeRepository.Find("NonexistentId")).Returns(null);

            // Act
            var iactionResult = UserController.DeleteUser("NonexistentId");

            // Assert
            Assert.IsType<NotFoundResult>(iactionResult);
        }

        [Fact]
        public void DeleteUser_WithExistentUser_NoContent()
        {
            // Arrange
            string id = "b4f5a-b4f5a-b4f5a-b4f5a-b4f5a";
            var fakeUser = A.Fake<User>();

            fakeUser.Id = id;
            fakeUser.FirstName = "Dennis";
            fakeUser.Surname = "Ritchie";
            fakeUser.Age = 70;

            var fakeRepository = A.Fake<IUserRepository>();
            var UserController = new UsersController(fakeRepository);
            A.CallTo(() => fakeRepository.Add(fakeUser));

            // Act
            var iactionResult = UserController.DeleteUser(id);

            // Assert
            Assert.IsType<NoContentResult>(iactionResult);
        }

    }

}
