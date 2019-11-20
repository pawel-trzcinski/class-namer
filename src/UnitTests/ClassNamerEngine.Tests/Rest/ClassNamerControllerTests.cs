using ClassNamerEngine.Puller;
using ClassNamerEngine.Rest;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace ClassNamerEngine.Tests.Rest
{
    [TestFixture]
    public class ClassNamerControllerTests
    {
        [Test]
        public void TestReturns200()
        {
            bool pullerExecuted = false;
            bool builderExecuted = false;
            string randomName = Guid.NewGuid().ToString();
            string randomHtml = Guid.NewGuid().ToString();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName()).Callback(() => pullerExecuted = true).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Callback(() => builderExecuted = true).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            StatusCodeResult result = controller.Test();

            Assert.AreEqual(200, result.StatusCode);
            Assert.IsFalse(pullerExecuted);
            Assert.IsFalse(builderExecuted);
        }

        [Test]
        public void RestReturnsRandomString()
        {
            bool pullerExecuted = false;
            bool builderExecuted = false;
            string randomName = Guid.NewGuid().ToString();
            string randomHtml = Guid.NewGuid().ToString();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName()).Callback(() => pullerExecuted = true).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Callback(() => builderExecuted = true).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            ContentResult result = controller.GetRandomClassName();

            Assert.AreEqual(randomName, result.Content);
            Assert.AreEqual(ClassNamerController.TEXT_PLAIN, result.ContentType);
            Assert.IsTrue(pullerExecuted);
            Assert.IsFalse(builderExecuted);
        }

        [Test]
        public void HtmlReturnsRandomHtml()
        {
            bool pullerExecuted = false;
            bool builderExecuted = false;
            string randomName = Guid.NewGuid().ToString();
            string randomHtml = Guid.NewGuid().ToString();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName()).Callback(() => pullerExecuted = true).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Callback(() => builderExecuted = true).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            ContentResult result = controller.GetRandomClassNameHtml();

            Assert.AreEqual(randomHtml, result.Content);
            Assert.AreEqual(ClassNamerController.TEXT_HTML, result.ContentType);
            Assert.IsTrue(pullerExecuted);
            Assert.IsTrue(builderExecuted);
        }

        [Test]
        public void RobotsReturnsText()
        {
            string randomName = Guid.NewGuid().ToString();
            string randomHtml = Guid.NewGuid().ToString();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName()).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            ContentResult result = controller.Robots();

            Assert.IsFalse(String.IsNullOrWhiteSpace(result.Content));
            Assert.AreEqual(ClassNamerController.TEXT_PLAIN, result.ContentType);
        }
    }
}