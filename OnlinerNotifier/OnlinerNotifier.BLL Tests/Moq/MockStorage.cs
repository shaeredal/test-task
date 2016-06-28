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
        }

        private void GenerateUnitOfWorkMock()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.Users).Returns(userRepositoryMock.Object);
        }
    }
}
