using FluentValidation;
using FluentValidation.Results;
using BraintreeSample.APIHelper.Entities;
using BraintreeSample.APIHelper.Repositories;
using BraintreeSample.APIHelper.Services;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Tests.Services
{
    public class SampleEntity : BaseEntity
    {
        public int sampleInt { get; set; }
        public string sampleStr { get; set; }
        public bool sampleBool { get; set; }
        public DateTime sampleDateTime { get; set; }
    }
    public class SampleService : BaseService<SampleEntity>
    {
        public SampleService(ILogger logger, IRepository<SampleEntity> repository) : base(logger, repository)
        {
        }
    }
    public class BaseServiceTest
    {
        [Test]
        public void CreateShouldReturnNotOkWhenInsertOperationFailed()
        {
            var validationMock = new Mock<IValidator<SampleEntity>>();
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new SampleEntity();
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Create(requestObject)).Returns(0);
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Create(requestObject);

            // Then
            Assert.False(result.IsSuccessed);
        }
        [Test]
        public void CreateShouldReturnOkWhenInsertOperationSucceeded()
        {
            var validationMock = new Mock<IValidator<SampleEntity>>();
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new SampleEntity();
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Create(requestObject)).Returns(1);
            repositoryMock.Setup(v => v.Read(1)).Returns(new SampleEntity() { ID = 1 });
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Create(requestObject);

            // Then
            Assert.True(result.IsSuccessed);
            Assert.AreEqual(1, result.Data.ID);
        }
        [Test]
        public void ReadWithIdShouldReturnNotOkWhenReadOperationFailed()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = 1;
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Read(requestObject));
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Read(requestObject);

            // Then
            Assert.False(result.IsSuccessed);
        }
        [Test]
        public void ReadWithIdShouldReturnOkWhenReadOperationSucceeded()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = 1;
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Read(requestObject)).Returns(new SampleEntity());
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Read(requestObject);

            // Then
            Assert.True(result.IsSuccessed);
        }
        [Test]
        public void ReadWithGuidShouldReturnNotOkWhenReadOperationFailed()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new Guid("2d62fa93-1be2-4e71-a8df-531b2fc278c3");
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Read(requestObject));
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Read(requestObject);

            // Then
            Assert.False(result.IsSuccessed);
        }
        [Test]
        public void ReadWithGuidShouldReturnOkWhenReadOperationSucceeded()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new Guid("2d62fa93-1be2-4e71-a8df-531b2fc278c3");
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Read(requestObject)).Returns(new SampleEntity());
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Read(requestObject);

            // Then
            Assert.True(result.IsSuccessed);
        }
        [Test]
        public void UpdateShouldReturnNotOkWhenInsertOperationFailed()
        {
            var validationMock = new Mock<IValidator<SampleEntity>>();
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new SampleEntity();
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Update(requestObject));
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Update(requestObject);

            // Then
            Assert.False(result.IsSuccessed);
        }
        [Test]
        public void UpdateShouldReturnOkWhenInsertOperationSucceeded()
        {
            var validationMock = new Mock<IValidator<SampleEntity>>();
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new SampleEntity() { ID = 1 };
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Update(requestObject)).Returns(true);
            repositoryMock.Setup(v => v.Read(1)).Returns(new SampleEntity() { ID = 1 });
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Update(requestObject);

            // Then
            Assert.True(result.IsSuccessed);
            Assert.AreEqual(1, result.Data.ID);
        }
        [Test]
        public void DeleteWithIdShouldReturnNotOkWhenDeleteOperationFailed()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = 1;
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Delete(requestObject));
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Delete(requestObject);

            // Then
            Assert.False(result.IsSuccessed);
        }
        [Test]
        public void DeleteWithIdShouldReturnOkWhenDeleteOperationSucceeded()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = 1;
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Delete(requestObject)).Returns(true);
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Delete(requestObject);

            // Then
            Assert.True(result.IsSuccessed);
        }
        [Test]
        public void DeleteWithGuidShouldReturnNotOkWhenDeleteOperationFailed()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new Guid("2d62fa93-1be2-4e71-a8df-531b2fc278c3");
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Delete(requestObject));
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Delete(requestObject);

            // Then
            Assert.False(result.IsSuccessed);
        }
        [Test]
        public void DeleteWithGuidShouldReturnOkWhenDeleteOperationSucceeded()
        {
            var loggerMock = new Mock<ILogger>();
            var repositoryMock = new Mock<IRepository<SampleEntity>>();
            var requestObject = new Guid("2d62fa93-1be2-4e71-a8df-531b2fc278c3");
            loggerMock.Setup(v => v.Error(It.IsAny<string>()));
            repositoryMock.Setup(v => v.Delete(requestObject)).Returns(true);
            var service = new SampleService(loggerMock.Object, repositoryMock.Object);

            // When
            var result = service.Delete(requestObject);

            // Then
            Assert.True(result.IsSuccessed);
        }
    }
}
