using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    [TestFixture]
    public class UserServiceTestBase
    {
        protected Mock<IUnitOfWork> unitOfWorkMock;
        protected Mock<IUserRepository> userRepositoryMock;
        protected UserService userService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.UnitOfWorkMock;
            userRepositoryMock = mockStorage.UserRepositoryMock;
            userService = new UserService(unitOfWorkMock.Object, 
                new UserMapper(new UserProductsMapper(new ProductMapper())),
                new EmailValidator());
        }
    }
}
