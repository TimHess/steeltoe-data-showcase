using Microsoft.Extensions.Logging;
using steeltoe.data.showcase.Domain;
using steeltoe.data.showcase.Repository;
using Steeltoe.Stream.Attributes;
using Steeltoe.Stream.Messaging;

namespace streaming.consumer.Consumer;

[EnableBinding(typeof(ISink))]
public class AccountConsumer
{

    private readonly ILogger<AccountConsumer> logger;

    public ITestDataRepository Repository;

    public AccountConsumer(ITestDataRepository repository, ILogger<AccountConsumer> logger)
    {
        Repository = repository;
        this.logger = logger;
    }

    [StreamListener(ISink.INPUT)]
    public void Accept(Account account)
    {
        logger?.LogInformation($"ACCOUNT: ${account}");
        Repository.Save(account);
    }
}