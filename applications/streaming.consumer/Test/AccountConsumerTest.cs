using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using steeltoe.data.showcase.Domain;
using steeltoe.data.showcase.Repository;
using streaming.consumer.Consumer;

namespace streaming.consumer.test;

[TestClass]
public class AccountConsumerTest
{
    private Account account;
        
        
    private Mock<ITestDataRepository> repository;

    private AccountConsumer subject;

    [TestInitialize]
    public void InitializeAccountConsumerTest()
    {
        repository = new Mock<ITestDataRepository>();

        account = new Account();
        subject = new AccountConsumer(repository.Object, null);
    }

    [TestMethod]
    public void Accept()
    {
        subject.Accept(account);

        repository.Verify( dataRepository => dataRepository.Save(It.IsAny<Account>()));
    }
}