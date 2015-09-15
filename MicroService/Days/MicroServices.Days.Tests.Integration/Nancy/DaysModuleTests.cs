using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Days.Nancy.Interfaces;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Days.Tests.Integration.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DaysModuleTests
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.FFF";

        private dynamic CreateDayToBeDeleted(Browser browser)
        {
            IDayForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post("/days/",
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            dynamic actual = ToDynamic(result.Body.AsString());

            return actual;
        }

        private IDayForResponse CreateModelForPutTest()
        {
            var model = new UpdateDayModel
                        {
                            Date = DateTime.Now,
                            DoctorId = 1
                        };

            return model;
        }

        [Fact]
        public void Should_return_status_OK_when_slot_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            dynamic slot = CreateDayToBeDeleted(browser);
            int slotId = Convert.ToInt32(slot [ "Id" ].Value);

            // When
            BrowserResponse result = browser.Delete("/days/" + slotId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        [Fact]
        public void Should_return_JSON_string_when_slot_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            dynamic day = CreateDayToBeDeleted(browser);
            int dayId = Convert.ToInt32(day [ "Id" ].Value);
            dynamic expected = CreatedExpectedDayFor(day);

            // When
            BrowserResponse result = browser.Delete("/days/" + dayId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertDay(expected,
                      actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_slot_with_id_exists()
        {
            // Given
            dynamic expected = CreateExpectedJsonStringForDayOne();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/days/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertDay(expected,
                      actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            dynamic expected = CreatedExpectedJsonStringForList();
            Browser browser = CreateBrowser();

            // When

            BrowserResponse result = browser.Get("/days/",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertDays(expected,
                       actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_list_for_day_is_requested()
        {
            // Given
            dynamic expected = CreatedExpectedJsonStringForListForDay();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/days/2015-06-30",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertDays(expected,
                       actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_list_for_day_and_doctor_requested()
        {
            // Given
            dynamic expected = CreatedExpectedJsonStringForListForDayAndDoctor();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/days/2015-06-30/doctors",
                                                 with =>
                                                 {
                                                     with.Query("doctorId",
                                                                "1");
                                                     with.HttpRequest();
                                                 });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertDays(expected,
                       actual);
        }

        [Theory]
        [InlineData("/days/")]
        [InlineData("/days/1")]
        [InlineData("/days/2015-06-30")]
        [InlineData("/days/2015-06-30/doctors")]
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
        [InlineData("/days/", HttpStatusCode.OK)]
        [InlineData("/days/1", HttpStatusCode.OK)]
        [InlineData("/days/-1", HttpStatusCode.NotFound)]
        [InlineData("/days/2015-06-30", HttpStatusCode.OK)]
        [InlineData("/days/2015-06-30/doctors", HttpStatusCode.OK)]
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

        [Theory]
        [InlineData("/days/2015-06-30/doctors", "doctorId", "1")]
        public void Should_return_status_OK_when_requested_with_query([NotNull] string url,
                                                                      [NotNull] string name,
                                                                      [NotNull] string value)
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get(url,
                                                 with =>
                                                 {
                                                     with.Query(name,
                                                                value);
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        [Fact]
        public void Should_update_database_when_days_is_updated()
        {
            IDayForResponse model = null;
            dynamic actual;
            dynamic expected;

            try
            {
                // Given
                Browser browser = CreateBrowser();
                dynamic slot = CreateDayToBeDeleted(browser);
                model = CreateModelForUpdateTest(slot);
                expected = CreatedExpectedDoctorForUpdate(model);

                // When
                BrowserResponse result = browser.Put("/days/",
                                                     with =>
                                                     {
                                                         with.JsonBody(model);
                                                     });

                // Then
                actual = ToDynamic(result.Body.AsString());
            }
            finally
            {
                if ( model != null )
                {
                    DeleteDayById(model.Id);
                }
            }

            // Then
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            AssertDay(expected,
                      actual);
        }

        private void AssertDay(dynamic expected,
                               dynamic actual)
        {
            Console.WriteLine("Comparing days with id {0} and {1} (ignoring ids)...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            var expectedId = ( int ) ( expected [ "Id" ].Value );
            var actualId = ( int ) ( actual [ "Id" ].Value );
            Assert.Equal(expectedId,
                         actualId);

            var expectedEndDateTime = ( DateTime ) ( expected [ "Date" ].Value );
            var actualEndDateTime = ( DateTime ) ( actual [ "Date" ].Value );
            Assert.True(expectedEndDateTime.Date == actualEndDateTime.Date,
                        "EndDateTime.Date");

            var expectedDoctorId = ( int ) expected [ "DoctorId" ].Value;
            var actualDoctorId = ( int ) actual [ "DoctorId" ].Value;
            Assert.Equal(expectedDoctorId,
                         actualDoctorId);
        }

        private void DeleteDayById(int dayId)
        {
            Browser browser = CreateBrowser();

            BrowserResponse result = browser.Delete("/days/" + dayId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        private IDayForResponse CreateModelForUpdateTest(dynamic day)
        {
            var model = new UpdateDayModel
                        {
                            Id = ( int ) ( day [ "Id" ].Value ), // todo change to long
                            Date = DateTime.Now.AddDays(1),
                            DoctorId = 2
                        };

            return model;
        }

        private dynamic CreatedExpectedDoctorForUpdate(IDayForResponse day)
        {
            DateTime date = day.Date;

            string json = "{" +
                          "\"Id\":" + day.Id + "," +
                          "\"Date\":\"" + date.ToString(DateTimeFormat) + "\"," +
                          "\"DoctorId\":" + day.DoctorId +
                          "}";

            return ToDynamic(json);
        }

        private void AssertDays(dynamic expected,
                                dynamic actual)
        {
            var expectedList = new List <dynamic>();
            foreach ( dynamic expectedSlot in expected )
            {
                expectedList.Add(expectedSlot);
            }

            var actualList = new List <dynamic>();
            foreach ( dynamic actualSlot in actual )
            {
                actualList.Add(actualSlot);
            }

            Assert.True(expectedList.Count == actualList.Count,
                        "count");

            foreach ( dynamic expectedSlot in expectedList )
            {
                var expectedSlotId = ( int ) ( expectedSlot [ "Id" ].Value );

                object compareToSlot = GetDayWithId(actualList,
                                                    expectedSlotId);

                AssertDay(expectedSlot,
                          compareToSlot);
            }
        }

        private object GetDayWithId(List <dynamic> list,
                                    int id)
        {
            return list.FirstOrDefault(slot => id == ( int ) ( slot [ "Id" ].Value ));
        }

        private dynamic CreatedExpectedJsonStringForListForDayAndDoctor()
        {
            string json = "[" +
                          "{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1}" +
                          "]";

            return ToDynamic(json);
        }

        private dynamic CreatedExpectedJsonStringForList()
        {
            string json = "[" +
                          "{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1}," +
                          "{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2}," +
                          "{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3}," +
                          "{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":2,\"Id\":4}," +
                          "{\"Date\":\"2015-08-03T00:00:00\",\"DoctorId\":3,\"Id\":5}" +
                          "]";

            return ToDynamic(json);
        }

        private dynamic CreatedExpectedJsonStringForListForDay()
        {
            string json = "[" +
                          "{\"Date\": \"2015-06-30T00:00:00\", \"DoctorId\": 1, \"Id\": 1}," +
                          "{\"Date\": \"2015-06-30T00:00:00\", \"DoctorId\": 2, \"Id\": 4}" +
                          "]";

            return ToDynamic(json);
        }

        private static dynamic ToDynamic(string json)
        {
            dynamic data = JsonConvert.DeserializeObject(json);

            return data;
        }

        private static dynamic CreateExpectedJsonStringForDayOne()
        {
            var json = "{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1}";

            return ToDynamic(json);
        }

        private static Browser CreateBrowser()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper,
                                      to => to.Accept("application/json"));
            return browser;
        }

        private dynamic CreatedExpectedDayFor(dynamic day)
        {
            var id = ( int ) day [ "Id" ].Value;
            var date = ( DateTime ) ( day [ "Date" ].Value );
            var doctorId = ( int ) ( day [ "DoctorId" ].Value );

            string json = "{" +
                          "\"Id\": " + id + "," +
                          "\"Date\": \"" + date.ToString(DateTimeFormat) + "\"," +
                          "\"DoctorId\": " + doctorId +
                          "}";

            return ToDynamic(json);
        }

        private class UpdateDayModel : IDayForResponse
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int DoctorId { get; set; }
        }
    }
}