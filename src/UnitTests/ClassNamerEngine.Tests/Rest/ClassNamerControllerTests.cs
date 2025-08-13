using ClassNamerEngine.Puller;
using ClassNamerEngine.Rest;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using AutoFixture;

namespace ClassNamerEngine.Tests.Rest
{
    [TestFixture]
    public class ClassNamerControllerTests
    {
        private static readonly Fixture _fixture = new Fixture();
        
        private static IEnumerable<string> RequiredList
        {
            get
            {
                yield return null;
                yield return String.Empty;
                yield return "  ";
                yield return "\t\n\r";
                yield return "\t  ";
                yield return _fixture.Create<string>();
                yield return _fixture.Create<string>();
                yield return _fixture.Create<string>();
            }
        }
        
        [Test]
        public void TestReturns200()
        {
            bool pullerExecuted = false;
            bool builderExecuted = false;
            string randomName = _fixture.Create<string>();
            string randomHtml = _fixture.Create<string>();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName(It.IsAny<string>())).Callback(() => pullerExecuted = true).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Callback(() => builderExecuted = true).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            StatusCodeResult result = controller.Test();

            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(!pullerExecuted);
            Assert.That(!builderExecuted);
        }

        [Test]
        public void RestReturnsRandomString([ValueSource(nameof(RequiredList))]string emptyRequired)
        {
            bool pullerExecuted = false;
            bool builderExecuted = false;
            string randomName = _fixture.Create<string>();
            string randomHtml = _fixture.Create<string>();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName(It.IsAny<string>())).Callback(() => pullerExecuted = true).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Callback(() => builderExecuted = true).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            ContentResult result = controller.GetRandomClassName(emptyRequired);

            Assert.That(result.Content, Is.EqualTo(randomName));
            Assert.That(result.ContentType, Is.EqualTo(ClassNamerController.TextPlain));
            Assert.That(pullerExecuted);
            Assert.That(!builderExecuted);
        }

        [Test]
        public void HtmlReturnsRandomHtml([ValueSource(nameof(RequiredList))]string emptyRequired)
        {
            bool pullerExecuted = false;
            bool builderExecuted = false;
            string randomName = _fixture.Create<string>();
            string randomHtml = _fixture.Create<string>();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName(It.IsAny<string>())).Callback(() => pullerExecuted = true).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Callback(() => builderExecuted = true).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            ContentResult result = controller.GetRandomClassNameHtml(emptyRequired);

            Assert.That(result.Content, Is.EqualTo(randomHtml));
            Assert.That(result.ContentType, Is.EqualTo(ClassNamerController.TextHtml));
            Assert.That(pullerExecuted);
            Assert.That(builderExecuted);
        }

        [Test]
        public void RobotsReturnsText()
        {
            string randomName = _fixture.Create<string>();
            string randomHtml = _fixture.Create<string>();

            Mock<IRandomNamePuller> randomNamePullerMock = new Mock<IRandomNamePuller>();
            randomNamePullerMock.Setup(p => p.GetRandomClassName(It.IsAny<string>())).Returns(randomName);
            Mock<IHtmlBuilder> htmlBuilderMock = new Mock<IHtmlBuilder>();
            htmlBuilderMock.Setup(p => p.BuildHtml(randomName)).Returns(randomHtml);

            ClassNamerController controller = new ClassNamerController(randomNamePullerMock.Object, htmlBuilderMock.Object);
            ContentResult result = controller.Robots();

            Assert.That(!String.IsNullOrWhiteSpace(result.Content));
            Assert.That(result.ContentType, Is.EqualTo(ClassNamerController.TextPlain));
        }
    }
}