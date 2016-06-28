using System.Collections.Generic;
using Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Moq
{
    public class MockStorage
    {
        public MockStorage()
        {
            GenerateUserRepositoryMock();
            GenerateUnitOfWorkMock();
        }

        public Mock<IUserRepository> UserRepositoryMock { get; private set; }

        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        private void GenerateUserRepositoryMock()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
            UserRepositoryMock.Setup(ur => ur.Get(1)).Returns(() => new User() { Id = 1, FirstName = "TestName", LastName = "TestLastName"});
            UserRepositoryMock.Setup(ur => ur.Get(It.Is<int>(i => i != 1))).Returns(() => null);
            var userList = GenerateUserList();
            UserRepositoryMock.Setup(ur => ur.GetAll()).Returns(() => userList);
            UserRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>()));
        }

        private List<User> GenerateUserList()
        {
            var userList = new List<User>();
            userList.Add(new User() { Id = 1, SocialId = "qwerty" });
            userList.Add(new User() { Id = 2, SocialId = "7788" });
            userList.Add(new User() { Id = 42, SocialId = "not 42" });
            return userList;
        }

        private void GenerateUnitOfWorkMock()
        {
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkMock.Setup(m => m.Users).Returns(UserRepositoryMock.Object);
        }
    }
}
