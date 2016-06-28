using System.Collections.Generic;
using Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Moq
{
    public class MockStorage
    {
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;

        public MockStorage()
        {
            GenerateUserRepositoryMock();
            GenerateUnitOfWorkMock();
        }

        public Mock<IUserRepository> GetUserRepositoryMock => userRepositoryMock;

        public Mock<IUnitOfWork> GetUnitOfWorkMock => unitOfWorkMock;

        private void GenerateUserRepositoryMock()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.Get(1)).Returns(() => new User() { Id = 1, FirstName = "TestName" });
            userRepositoryMock.Setup(ur => ur.Get(It.Is<int>(i => i != 1))).Returns(() => null);
            var userList = GenerateUserList();
            userRepositoryMock.Setup(ur => ur.GetAll()).Returns(() => userList);
            userRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>()));
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
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.Users).Returns(userRepositoryMock.Object);
        }
    }
}
