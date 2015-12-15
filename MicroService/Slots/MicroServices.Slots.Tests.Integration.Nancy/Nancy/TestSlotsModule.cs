using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Slots.Tests.Integration.Nancy.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class TestSlotsModule
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.FFF";

        [Fact]
        public void Should_return_JSON_string_when_slot_with_id_exists()
        {
            // Given
            dynamic expected = CreateExpectedJsonStringForSlotOne();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/slots/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertSlot(expected,
                       actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            dynamic expected = CreatedExpectedJsonStringForList();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/slots/",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertSlots(expected,
                        actual);
        }

        [Fact]
        public void Should_return_JSON_string_when_post_requested()
        {
            dynamic actual = null;

            try
            {
                // Given
                ISlotForResponse model = CreateModelForPutTest();
                dynamic expected = CreatedExpectedSlotForPost(model);
                Browser browser = CreateBrowser();

                // When
                BrowserResponse result = browser.Post("/slots/",
                                                      with =>
                                                      {
                                                          with.JsonBody(model);
                                                      });

                actual = ToDynamic(result.Body.AsString());

                // Then
                AssertSlotIgnoringIds(expected,
                                      actual);
            }
            finally
            {
                if ( actual != null )
                {
                    var slotId = ( int ) actual [ "Id" ].Value;

                    DeleteSlotById(slotId);
                }
            }
        }

        [Theory]
        [InlineData("/slots/")]
        [InlineData("/slots/1")]
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
        [InlineData("/slots/", HttpStatusCode.OK)]
        [InlineData("/slots/1", HttpStatusCode.OK)]
        [InlineData("/slots/-1", HttpStatusCode.NotFound)]
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

        [Fact]
        public void Should_return_status_OK_when_slot_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            dynamic slot = CreateSlotToBeDeleted(browser);
            int slotId = Convert.ToInt32(slot [ "Id" ].Value);

            // When
            BrowserResponse result = browser.Delete("/slots/" + slotId,
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
            dynamic slot = CreateSlotToBeDeleted(browser);
            int slotId = Convert.ToInt32(slot [ "Id" ].Value);
            dynamic expected = CreatedExpectedSlotFor(slot);

            // When
            BrowserResponse result = browser.Delete("/slots/" + slotId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            dynamic actual = ToDynamic(result.Body.AsString());

            // Then
            AssertSlot(expected,
                       actual);
        }

        [Fact]
        public void Should_update_database_when_doctor_is_updated()
        {
            ISlotForResponse model = null;
            dynamic actual;
            dynamic expected;

            try
            {
                // Given
                Browser browser = CreateBrowser();
                dynamic slot = CreateSlotToBeDeleted(browser);
                model = CreateModelForUpdateTest(slot);
                expected = CreatedExpectedDoctorForUpdate(model);

                // When
                BrowserResponse result = browser.Put("/slots/",
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
                    DeleteSlotById(model.Id);
                }
            }

            // Then
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            AssertSlot(expected,
                       actual);
        }

        private ISlotForResponse CreateModelForUpdateTest(dynamic slot)
        {
            var model = new UpdateSlotModel
                        {
                            Id = ( int ) slot [ "Id" ].Value, // todo change to long
                            DayId = ( int ) slot [ "DayId" ].Value, // todo change to long
                            StartDateTime = DateTime.Now.AddDays(1),
                            EndDateTime = DateTime.Now.AddDays(1).AddMinutes(15),
                            Status = SlotStatus.Unknown
                        };

            return model;
        }

        private dynamic CreatedExpectedDoctorForUpdate(ISlotForResponse slot)
        {
            DateTime endDateTime = slot.EndDateTime;
            DateTime startDateTime = slot.StartDateTime;

            string json = "{" +
                          "\"Id\":" + slot.Id + "," +
                          "\"DayId\":" + slot.DayId + "," +
                          "\"EndDateTime\":\"" + endDateTime.ToString(DateTimeFormat) + "\"," +
                          "\"StartDateTime\":\"" + startDateTime.ToString(DateTimeFormat) + "\"," +
                          "\"Status\":" + ( int ) slot.Status +
                          "}";

            return ToDynamic(json);
        }


        private void AssertSlots(dynamic expected,
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
                var expectedSlotId = ( int ) expectedSlot [ "Id" ].Value;

                object compareToSlot = GetSlotWithId(actualList,
                                                     expectedSlotId);

                AssertSlot(expectedSlot,
                           compareToSlot);
            }
        }

        private object GetSlotWithId(List <dynamic> list,
                                     int id)
        {
            return list.FirstOrDefault(slot => id == ( int ) slot [ "Id" ].Value);
        }

        private dynamic CreatedExpectedJsonStringForList()
        {
            string json = "[" +
                          "{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}," +
                          "{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1}," +
                          "{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}," +
                          "{\"Id\":4,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:30:00\",\"StartDateTime\":\"2015-07-30T14:15:00\",\"Status\":1}," +
                          "{\"Id\":5,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:45:00\",\"StartDateTime\":\"2015-07-30T14:30:00\",\"Status\":2}," +
                          "{\"Id\":6,\"DayId\":4,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}," +
                          "{\"Id\":7,\"DayId\":5,\"EndDateTime\":\"2015-08-03T09:15:00\",\"StartDateTime\":\"2015-08-03T09:00:00\",\"Status\":1}," +
                          "{\"Id\":8,\"DayId\":5,\"EndDateTime\":\"2015-08-03T09:30:00\",\"StartDateTime\":\"2015-08-03T09:15:00\",\"Status\":1}" +
                          "]";

            return ToDynamic(json);
        }

        private static dynamic ToDynamic(string json)
        {
            dynamic data = JsonConvert.DeserializeObject(json);

            return data;
        }

        private static dynamic CreateExpectedJsonStringForSlotOne()
        {
            string json = "{" +
                          "\"Id\":1," +
                          "\"DayId\":1," +
                          "\"EndDateTime\":\"2015-06-30T09:15:00\"," +
                          "\"StartDateTime\":\"2015-06-30T09:00:00\"," +
                          "\"Status\":1" +
                          "}";

            return ToDynamic(json);
        }


        private static void AssertSlotIgnoringIds(dynamic expected,
                                                  dynamic actual)
        {
            Console.WriteLine("Comparing slots with id {0} and {1} (ignoring ids)...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            Assert.True(expected [ "DayId" ].Value == actual [ "DayId" ].Value,
                        "DayId");

            var expectedEndDateTime = ( DateTime ) expected [ "EndDateTime" ].Value;
            var actualEndDateTime = ( DateTime ) actual [ "EndDateTime" ].Value;
            Assert.True(expectedEndDateTime.Date == actualEndDateTime.Date,
                        "EndDateTime.Date");
            Assert.True(expectedEndDateTime.ToShortTimeString() == actualEndDateTime.ToShortTimeString(),
                        "EndDateTime.Time");

            var expectedStartDateTime = ( DateTime ) expected [ "StartDateTime" ].Value;
            var actualStartDateTime = ( DateTime ) actual [ "StartDateTime" ].Value;
            Assert.True(expectedStartDateTime.Date == actualStartDateTime.Date,
                        "EndDateTime.Date");
            Assert.True(expectedStartDateTime.ToShortTimeString() == actualStartDateTime.ToShortTimeString(),
                        "EndDateTime.Time");

            Assert.True(expected [ "Status" ].Value == actual [ "Status" ].Value,
                        "Status");
        }

        private static void AssertSlot(dynamic expected,
                                       dynamic actual)
        {
            Console.WriteLine("Comparing slots with id {0} and {1}...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            Assert.True(expected [ "Id" ].Value == actual [ "Id" ].Value,
                        "Id");

            AssertSlotIgnoringIds(expected,
                                  actual);
        }

        private static Browser CreateBrowser()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper,
                                      to => to.Accept("application/json"));
            return browser;
        }

        private dynamic CreatedExpectedSlotFor(dynamic slot)
        {
            var id = ( int ) slot [ "Id" ].Value;
            var start = ( DateTime ) slot [ "StartDateTime" ].Value;
            var end = ( DateTime ) slot [ "EndDateTime" ].Value;

            string json = "{" +
                          "\"Id\": " + id + "," +
                          "\"DayId\": 1," +
                          "\"EndDateTime\": \"" + end.ToString(DateTimeFormat) + "\"," +
                          "\"StartDateTime\": \"" + start.ToString(DateTimeFormat) + "\"," +
                          "\"Status\": 1" +
                          "}";

            return ToDynamic(json);
        }

        private dynamic CreatedExpectedSlotForPost(ISlotForResponse slot)
        {
            int id = slot.Id;
            DateTime start = slot.StartDateTime;
            DateTime end = slot.EndDateTime;

            string json = "{" +
                          "\"Id\": " + id + "," +
                          "\"DayId\": 1," +
                          "\"EndDateTime\": \"" + end.ToString(DateTimeFormat) + "\"," +
                          "\"StartDateTime\": \"" + start.ToString(DateTimeFormat) + "\"," +
                          "\"Status\": 1" +
                          "}";

            return ToDynamic(json);
        }

        private dynamic CreateSlotToBeDeleted(Browser browser)
        {
            ISlotForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post("/slots/",
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            dynamic actual = ToDynamic(result.Body.AsString());

            return actual;
        }

        private ISlotForResponse CreateModelForPutTest()
        {
            var model = new UpdateSlotModel
                        {
                            DayId = 1,
                            StartDateTime = DateTime.Now,
                            EndDateTime = DateTime.Now.AddMinutes(15),
                            Status = SlotStatus.Open
                        };

            return model;
        }

        private void DeleteSlotById(int slotId)
        {
            Browser browser = CreateBrowser();

            BrowserResponse result = browser.Delete("/slots/" + slotId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

        private class UpdateSlotModel : ISlotForResponse
        {
            public int Id { get; set; }
            public int DayId { get; set; }
            public DateTime EndDateTime { get; set; }
            public DateTime StartDateTime { get; set; }
            public SlotStatus Status { get; set; }
        }
    }
}