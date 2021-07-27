using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySundial.Reflections.Api;
using MySundial.Reflections.Services.Api;
using MySundial.Reflections.Services.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Blob.Excel
{
    [Binding]
    public class Fn_UserSaveSteps
    {
        private readonly ILogger<UserDelete> loggerDelete = LoggerFactory.CreateLogger<UserDelete>();
        private readonly ILogger<UserSave> logger = LoggerFactory.CreateLogger<UserSave>();
        private readonly IConfiguration config = new AppConfigurationFactory().Create();

        public Guid SutKey { get; private set; }
        public User Sut { get; private set; }
        public IList<User> Suts { get; private set; } = new List<User>();
        public IList<User> RecycleBin { get; private set; } = new List<User>();

        public Fn_UserSaveSteps()
        {
            config = new AppConfigurationFactory().Create();
        }

        [Given(@"I have a new user for the Azure Function")]
        public void GivenIHaveANewUserForTheAzureFunction()
        {
            Sut = new User() {
                UserName = "UserCreateSteps Test",
                ExternalKey = Guid.NewGuid().ToString()
            };
        }

        [When(@"User is created via Azure Function")]
        public async Task WhenUserIsCreatedViaAzureFunction()
        {            
            var request = new HttpRequestFactory("post").CreateHttpRequest("code", config["Reflections:Shared:FunctionsCode"], Sut);
            var response = (OkObjectResult)await new UserSave(logger, config).Run(Sut, request);
            Assert.IsTrue(response.StatusCode == StatusCodes.Status200OK);
            Sut = (User)response.Value;
            Suts.Add(Sut);
            SutKey = Sut.RowKey;
            RecycleBin.Add(Sut);
        }

        [Then(@"the user is inserted to persistence from the Azure Function")]
        public void ThenTheUserIsInsertedToPersistenceFromTheAzureFunction()
        {
            Assert.IsTrue(SutKey != Guid.Empty);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (var item in RecycleBin)
            {
                var request = new HttpRequestFactory("delete").CreateHttpRequest("code", config["Reflections:Shared:FunctionsCode"], item);
                await new UserDelete(loggerDelete, config).Run(request);
            }
        }
    }
}
