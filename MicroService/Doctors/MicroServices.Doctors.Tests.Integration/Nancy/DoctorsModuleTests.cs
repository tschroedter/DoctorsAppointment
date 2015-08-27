using System;
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
        public void Should_return_JSON_string_when_doctor_is_created()
        {
            try
            {
                // Given
                dynamic expected = CreateExpectedResponseForCreate();
                Browser browser = CreateBrowser();

                // When
                BrowserResponse result = browser.Post("/doctors/",
                                                      with =>
                                                      {
                                                          with.HttpRequest();
                                                      });

                dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

                // Then
                XUnitDoctorsHelper.AssertDoctorIgnoreId(expected,
                                                        actual);
            }
            finally
            {
                DeleteDoctorToBeCreated();
            }
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists()
        {
            // Given
            dynamic expected = CreateExpectedResponseForMiller();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

            // Then
            XUnitDoctorsHelper.AssertDoctor(expected,
                                            actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            dynamic doctor = CreateDoctorToBeDeleted(browser);
            int doctorId = Convert.ToInt32(doctor [ "Id" ].Value);

            // When
            BrowserResponse result = browser.Delete("/doctors/" + doctorId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        private dynamic CreateDoctorToBeDeleted(Browser browser)
        {
            BrowserResponse result = browser.Post("/doctors/",
                                                  with =>
                                                  {
                                                      with.HttpRequest();
                                                  });

            dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

            return actual;
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_id_exists()
        {
            // Given
            dynamic expected = CreateExpectedResponseForMiller();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

            // Then
            XUnitDoctorsHelper.AssertDoctor(expected,
                                            actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            // todo convert to dynamic
            dynamic expected = CreateExpectedJsonStringForList();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

            // Then
            XUnitDoctorsHelper.AssertDoctors(expected,
                                             actual);
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

        private static dynamic CreateExpectedResponseForMiller()
        {
            var json = "{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1}";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private dynamic CreateExpectedJsonStringForList()
        {
            string json = "[" +
                          "{\"LastName\":\"Miller\",\"FirstName\":\"Mary\",\"Id\":1}," +
                          "{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2}" +
                          "]";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private dynamic CreateExpectedResponseForCreate()
        {
            var json = "{\"LastName\":\"LastName\",\"FirstName\":\"FirstName\",\"Id\":3}";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private void DeleteDoctorToBeCreated()
        {
            Browser browser = CreateBrowser();

            BrowserResponse existing = browser.Get("/doctors/LastName",
                                                   with =>
                                                   {
                                                       with.HttpRequest();
                                                   });

            if ( existing.StatusCode == HttpStatusCode.NotFound )
            {
                return;
            }

            dynamic doctor = XUnitDoctorsHelper.ToDynamic(existing.Body.AsString());
            int doctorId = Convert.ToInt32(doctor [ "Id" ].Value);

            BrowserResponse result = browser.Delete("/doctors/" + doctorId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }
    }
}