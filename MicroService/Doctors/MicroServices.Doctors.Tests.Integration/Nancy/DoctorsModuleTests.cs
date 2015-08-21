using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Nancy;
using Nancy.Testing;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Doctors.Tests.Integration.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DoctorsModuleTests
    {
        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists()
        {
            // Given
            string expected = CreateExpectedResponseForMiller();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_id_exists()
        {
            // Given
            string expected = CreateExpectedResponseForMiller();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Fact]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            string expected = CreateExpectedJsonStringForList();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Theory]
        [InlineData("/doctors/")]
        [InlineData("/doctors/1")]
        [InlineData("/doctors/Miller")]
        public void Should_return_JSON_when_requested([NotNull] string url)
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get(url,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal("application/json",
                         result.ContentType);
        }

        [Theory]
        [InlineData("/doctors/", HttpStatusCode.OK)]
        [InlineData("/doctors/1", HttpStatusCode.OK)]
        [InlineData("/doctors/-1", HttpStatusCode.NotFound)]
        [InlineData("/doctors/Unknown", HttpStatusCode.NotFound)]
        [InlineData("/doctors/Miller", HttpStatusCode.OK)]
        public void Should_return_status_OK_when_requested([NotNull] string url,
                                                           HttpStatusCode status)
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get(url,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(status,
                         result.StatusCode);
        }

        private static Browser CreateBrowser()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper,
                                      to => to.Accept("application/json"));
            return browser;
        }

        private static string CreateExpectedResponseForMiller()
        {
            return "{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1}";
        }

        private string CreateExpectedJsonStringForList()
        {
            return "[" +
                   "{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1}," +
                   "{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2}" +
                   "]";
        }
    }
}