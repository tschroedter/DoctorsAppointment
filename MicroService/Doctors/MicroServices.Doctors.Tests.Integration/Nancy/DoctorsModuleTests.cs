using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.Doctors.Nancy.Interfaces;
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
        public void Should_return_JSON_string_when_doctor_is_created_for_put()
        {
            dynamic actual = null;

            try
            {
                // Given
                IDoctorForResponse model = CreateModelForPutTest();
                dynamic expected = CreateExpectedResponseForPutTest();
                Browser browser = CreateBrowser();

                // When
                BrowserResponse result = browser.Put("/doctors/",
                                                     with =>
                                                     {
                                                         with.JsonBody(model);
                                                     });

                actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

                // Then
                XUnitDoctorsHelper.AssertDoctorIgnoreId(expected,
                                                        actual);
            }
            finally
            {
                if ( actual != null )
                {
                    int id = Convert.ToInt32(actual [ "Id" ].Value);

                    DeleteDoctorById(id);
                }
            }
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_is_created()
        {
            dynamic actual = null;

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

                actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

                // Then
                XUnitDoctorsHelper.AssertDoctorIgnoreId(expected,
                                                        actual);
            }
            finally
            {
                if ( actual != null )
                {
                    var doctorId = ( long ) actual [ "Id" ].Value;

                    DeleteDoctorById(doctorId);
                }
            }
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists()
        {
            // Given
            dynamic expected = CreateExpectedResponseForMiller();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/byLastName/Miller",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic doctors = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());
            dynamic actual = doctors [ 0 ];

            // Then
            XUnitDoctorsHelper.AssertDoctor(expected,
                                            actual);
        }

        [Fact]
        public void Should_return_status_OK_when_doctor_is_deleted()
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

        [Fact]
        public void Should_return_JSON_string_when_doctor_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            dynamic doctor = CreateDoctorToBeDeleted(browser);
            int doctorId = Convert.ToInt32(doctor [ "Id" ].Value);
            dynamic expected = CreatedExpectedDoctorFor(doctorId);

            // When
            BrowserResponse result = browser.Delete("/doctors/" + doctorId,
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
        public void Should_return_JSON_string_when_doctor_is_updated()
        {
            IDoctorForResponse model = null;

            try
            {
                // Given
                Browser browser = CreateBrowser();
                dynamic doctor = CreateDoctorToBeDeleted(browser);
                model = CreateModelForUpdate(doctor);

                dynamic expected = CreatedExpectedDoctorForUpdate(model.Id);

                // When
                BrowserResponse result = browser.Put("/doctors/",
                                                     with =>
                                                     {
                                                         with.JsonBody(model);
                                                     });

                dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

                // Then
                XUnitDoctorsHelper.AssertDoctor(expected,
                                                actual);
            }
            finally
            {
                if ( model != null )
                {
                    DeleteDoctorById(model.Id);
                }
            }
        }

        [Fact]
        public void Should_update_database_when_doctor_is_updated()
        {
            IDoctorForResponse model = null;

            try
            {
                // Given
                Browser browser = CreateBrowser();
                dynamic doctor = CreateDoctorToBeDeleted(browser);
                model = CreateModelForUpdate(doctor);

                // When
                BrowserResponse result = browser.Put("/doctors/",
                                                     with =>
                                                     {
                                                         with.JsonBody(model);
                                                     });

                // Then
                Assert.Equal(HttpStatusCode.OK,
                             result.StatusCode);

                // *** Post-conditions ***
                // Given
                dynamic expected = CreatedExpectedDoctorForUpdate(model.Id);

                // When
                result = browser.Get("/doctors/" + model.Id,
                                     with =>
                                     {
                                         with.HttpRequest();
                                     });

                dynamic actual = XUnitDoctorsHelper.ToDynamic(result.Body.AsString());

                // Then
                XUnitDoctorsHelper.AssertDoctor(expected,
                                                actual);
            }
            finally
            {
                if ( model != null )
                {
                    DeleteDoctorById(model.Id);
                }
            }
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

        [Fact]
        public void Should_return_JSON_string_when_search_by_last_name_returns_multiple()
        {
            // Given
            dynamic expected = CreateExpectedJsonStringForSearchForSmith();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/byLastName/Smith",
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
        [InlineData("/doctors/byLastName/Miller")]
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
        [InlineData("/doctors/byLastName/Unknown", HttpStatusCode.OK)]
        [InlineData("/doctors/byLastName/Miller", HttpStatusCode.OK)]
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
                          "{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2}," +
                          "{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}" +
                          "]";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private dynamic CreateExpectedJsonStringForSearchForSmith()
        {
            string json = "[" +
                          "{\"LastName\":\"Smith\",\"FirstName\":\"Will\",\"Id\":2}," +
                          "{\"LastName\":\"Smith\",\"FirstName\":\"Jane\",\"Id\":3}" +
                          "]";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private dynamic CreateExpectedResponseForCreate()
        {
            var json = "{\"LastName\":\"LastName\",\"FirstName\":\"FirstName\",\"Id\":3}";

            return XUnitDoctorsHelper.ToDynamic(json);
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

        private IDoctorForResponse CreateModelForPutTest()
        {
            var model = new UpdateDoctorModel
                        {
                            FirstName = "Create",
                            LastName = "Test"
                        };

            return model;
        }

        private dynamic CreateExpectedResponseForPutTest()
        {
            string json = "{" +
                          "\"FirstName\":\"Create\"," +
                          "\"LastName\":\"Test\"," +
                          "\"Id\":-1" +
                          "}";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private IDoctorForResponse CreateModelForUpdate(dynamic doctor)
        {
            int doctorId = Convert.ToInt32(doctor [ "Id" ].Value);

            var model = new UpdateDoctorModel
                        {
                            Id = doctorId,
                            FirstName = "Update",
                            LastName = "Test"
                        };

            return model;
        }

        private dynamic CreatedExpectedDoctorForUpdate(int doctorId)
        {
            string json = "{" +
                          "\"FirstName\":\"Update\"," +
                          "\"LastName\":\"Test\"," +
                          "\"Id\":" + doctorId +
                          "}";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private dynamic CreatedExpectedDoctorFor(int doctorId)
        {
            string json = "{" +
                          "\"FirstName\":\"FirstName\"," +
                          "\"LastName\":\"LastName\"," +
                          "\"Id\":" + doctorId +
                          "}";

            return XUnitDoctorsHelper.ToDynamic(json);
        }

        private void DeleteDoctorById(long doctorId)
        {
            Browser browser = CreateBrowser();
            BrowserResponse result = browser.Delete("/doctors/" + doctorId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        private class UpdateDoctorModel : IDoctorForResponse
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public int Id { get; set; }
        }
    }
}