using System.Collections.Generic;
using steeltoe.data.showcase.Controllers;
using steeltoe.data.showcase.Domain;
using steeltoe.data.showcase.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace steeltoe.data.showcase.test.Controllers;

[TestClass]
public class TestDataControllerTest
{
    private Mock<ITestDataRepository> repository = new();
    private TestDataController subject;
    private Account testData;

    [TestInitialize]
    public void InitializeTestDataController()
    {
        repository = new Mock<ITestDataRepository>();
        subject = new TestDataController(repository.Object);
        testData = new Account
        {
            Id = 1
        };
    }
        
    [TestMethod]
    public void SaveData()
    {
        subject.PostData(testData);

        repository.Verify( testDataRepository => testDataRepository.Save(It.IsAny<Account>()));
    }

    [TestMethod]
    public void FindData()
    {
        repository.Setup( r => r.FindById(1)).Returns(testData);
        Assert.AreEqual(testData,subject.FindById(testData.Id));
    }

    [TestMethod]
    public void FindAll()
    {
            
        List<Account> expected = new() { testData };

        repository.Setup( r => r.FindAll()).Returns(expected);

        Assert.AreEqual(expected,subject.FindAll());
    }

    [TestMethod]
    public void DeleteById()
    {
        subject.DeleteById(1);

        repository.Verify( testDataRepository => testDataRepository.DeleteById(It.IsAny<int>()));
    }
}