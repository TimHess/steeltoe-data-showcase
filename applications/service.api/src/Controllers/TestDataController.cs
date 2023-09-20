using System.Collections.Generic;
using steeltoe.data.showcase.Domain;
using steeltoe.data.showcase.Repository;
using Microsoft.AspNetCore.Mvc;

namespace steeltoe.data.showcase.Controllers;

[ApiController]
[Route("[controller]")]
public class TestDataController
{
    private readonly ITestDataRepository repository;

    public TestDataController(ITestDataRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public void PostData(Account testData)
    {
        repository.Save(testData);
    }

    [HttpGet]
    [Route("{id}")]
    public Account FindById(int id)
    {
        return repository.FindById(id);
    }

    [HttpGet]
    public List<Account> FindAll()
    {
        return repository.FindAll();
    }

    [HttpDelete]
    [Route("{id}")]
    public void DeleteById(int id)
    {
        repository.DeleteById(id);
    }
}