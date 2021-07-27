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
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecRun.Common.Helper;

namespace GoodToCode.Shared.Blob.Excel
{
    [Binding]
    public class Fn_UserDeleteSteps
    {
        private readonly ILogger<UserDelete> logger = LoggerFactory.CreateLogger<UserDelete>();
        private readonly ILogger<UserGetByExternalKey> loggerGet = LoggerFactory.CreateLogger<UserGetByExternalKey>();
        private readonly IConfiguration config = new AppConfigurationFactory().Create();
        private readonly Fn_UserSaveSteps createSteps = new Fn_UserSaveSteps();
        private string _originalValue = string.Empty;
        public string SutExternalKey { get; private set; }
        public User Sut { get; private set; }
        public IList<User> Suts { get; private set; } = new List<User>();
        public IList<User> RecycleBin { get; private set; } = new List<User>();

        public Fn_UserDeleteSteps()
        {
            config = new AppConfigurationFactory().Create();
        }

        [Given(@"I have an non empty user key for the Azure Function to delete")]
        public async Task GivenIHaveAnNonEmptyUserKeyForTheAzureFunctionToDelete()
        {
            createSteps.GivenIHaveANewUserForTheAzureFunction();
            await createSteps.WhenUserIsCreatedViaAzureFunction();
            Suts = createSteps.Suts;
            Sut = createSteps.Sut;
            SutExternalKey = createSteps.Sut.ExternalKey;
            _originalValue = Sut.UserName;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(SutExternalKey));
        }

        [Given(@"the user key is provided for the Azure Function")]
        public void GivenTheUserKeyIsProvidedForTheAzureFunction()
        {
            Sut.UserName = $"Fn_UserDeleteSteps Test {Guid.NewGuid()}";
            Assert.IsTrue(Sut.UserName.IsNotNullOrWhiteSpace());
            Assert.IsTrue(Sut.UserName != _originalValue);
        }

        [When(@"User is deleted via Azure Function")]
        public async Task WhenUserIsDeletedViaAzureFunction()
        {
            var request = new HttpRequestFactory("delete").CreateHttpRequest("key", Sut.ExternalKey, Sut);
            var response = (OkResult)await new UserDelete(logger, config).Run(request);
            Assert.IsTrue(response.StatusCode == StatusCodes.Status200OK);
        }

        [Then(@"the user does not exist in persistence when queried from Azure Function")]
        public async Task ThenTheUserDoesNotExistInPersistenceWhenQueriedFromAzureFunction()
        {
            var request = new HttpRequestFactory("get").CreateHttpRequest("key", Sut.ExternalKey);
            var response = (NotFoundResult)await new UserGetByExternalKey(loggerGet, config).Run(request);
            Assert.IsTrue(response.StatusCode == StatusCodes.Status404NotFound);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
        }
    }
}
