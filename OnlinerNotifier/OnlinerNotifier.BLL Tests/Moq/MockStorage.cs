using System.Collections.Generic;
using System.Web.Mvc.Html;
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
            GenerateProductMock();
            GenerateUserMock();
            GenerateUserRepositoryMock();
            GenerateProductRepositoryMock();
            GenerateUnitOfWorkMock();   
        }

        public Mock<IUserRepository> UserRepositoryMock { get; private set; }

        public Mock<User> UserMock { get; private set; }

        public Mock<Product> ProductMock { get; private set; }

        public Mock<UserProduct> UserProductMock { get; private set; }

        public Mock<IRepository<Product>> ProductRepositoryMock { get; private set; }

        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        private void GenerateUserRepositoryMock()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
            UserRepositoryMock.Setup(ur => ur.Get(1)).Returns(() => UserMock.Object);
            UserRepositoryMock.Setup(ur => ur.Get(It.Is<int>(i => i != 1))).Returns(() => null);
            var userList = GenerateUserList();
            UserRepositoryMock.Setup(ur => ur.GetAll()).Returns(() => userList);
            UserRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>()));
            UserRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>()));
        }

        private void GenerateUserMock()
        {
            UserMock = new Mock<User>();
            UserMock.Object.Id = 1;
            UserMock.Object.FirstName = "TestName";
            UserMock.Object.LastName = "TestLastName";
            GenerateUserProductMock(ProductMock.Object, UserMock.Object);
            UserMock.Object.UserProducts.Add(UserProductMock.Object);
        }

        private void GenerateUserProductMock(Product product, User user)
        {
            UserProductMock = new Mock<UserProduct>();
            UserProductMock.Object.Id = 1;
            UserProductMock.Object.Product = product;
            UserProductMock.Object.User = user;
        }

        private void GenerateProductMock()
        {
            ProductMock = new Mock<Product>();
            ProductMock.Object.Id = 1;
            ProductMock.Object.Name = "TestProductName";    
        }

        private List<User> GenerateUserList()
        {
            var userList = new List<User>();
            userList.Add(new User() { Id = 1, SocialId = "qwerty" });
            userList.Add(new User() { Id = 2, SocialId = "7788" });
            userList.Add(new User() { Id = 42, SocialId = "not 42" });
            return userList;
        }

        private void GenerateProductRepositoryMock()
        {
            ProductRepositoryMock = new Mock<IRepository<Product>>();
            ProductRepositoryMock.Setup(pr => pr.Get(1)).Returns(() => ProductMock.Object);
            var productList = GenerateProductList();
            ProductRepositoryMock.Setup(pr => pr.GetAll()).Returns(() => productList);
            ProductRepositoryMock.Setup(pr => pr.Create(It.IsAny<Product>()));
        }

        private List<Product> GenerateProductList()
        {
            var productList = new List<Product>();
            productList.Add(new Product() { Id = 100, OnlinerId = 12345});
            productList.Add(new Product() { Id = 200, OnlinerId = 33});
            productList.Add(new Product() { Id = 300, OnlinerId = 1122 });
            return productList;
        }

        private void GenerateUnitOfWorkMock()
        {
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkMock.Setup(m => m.Users).Returns(UserRepositoryMock.Object);
            UnitOfWorkMock.Setup(m => m.Products).Returns(ProductRepositoryMock.Object);
            var userProducts = new List<UserProduct>();
            userProducts.Add(UserProductMock.Object);
            UnitOfWorkMock.Setup(m => m.UserProducts.GetAll())
                .Returns(() => userProducts);
        }
    }
}
