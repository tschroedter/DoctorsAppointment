using System.Diagnostics.CodeAnalysis;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace MicroServices.Doctors.Tests.Integration.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DoctorsModuleTests
    {
        [Fact]
        public void Should_return_status_OK_when_doctor_with_lastname_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        private static Browser CreateBrowser()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper,
                                      to => to.Accept("application/json"));
            return browser;
        }

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
        public void Should_return_JSON_when_doctor_with_lastname_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal("application/json",
                         result.ContentType);
        }

        [Fact]
        public void Should_return_status_NotFound_when_doctor_lastname_doesnot_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Unknown",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.NotFound,
                         result.StatusCode);
        }

        [Fact]
        public void Should_return_status_OK_when_doctor_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
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

        private static string CreateExpectedResponseForMiller()
        {
            return "{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1}";
        }

        [Fact]
        public void Should_return_status_NotFound_when_doctor_with_id_doesnot_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/-1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.NotFound,
                         result.StatusCode);
        }

        [Fact]
        public void Should_return_JSON_when_doctor_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal("application/json",
                         result.ContentType);
        }

        [Fact]
        public void Should_return_status_OK_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        [Fact]
        public void Should_return_JSON_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal("application/json",
                         result.ContentType);
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

        private string CreateExpectedJsonStringForList()
        {
            return "[" +
                   "{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1}," +
                   "{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2}" +
                   "]";
        }
    }
}