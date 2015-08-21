using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MicroServices.Slots.Tests.Integration.Nancy.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SlotsModuleTests
    {
        [Fact]
        public void Should_return_status_OK_when_slot_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/slots/1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }

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
        public void Should_return_status_NotFound_when_slot_id_doesnot_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/slots/-1",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.NotFound,
                         result.StatusCode);
        }

        [Fact]
        public void Should_return_JSON_when_slot_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/slots/1",
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
            BrowserResponse result = browser.Get("/slots/",
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
            BrowserResponse result = browser.Get("/slots/",
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
                var expectedSlotId = ( int ) ( expectedSlot [ "Id" ].Value );

                object compareToSlot = GetSlotWithId(actualList,
                                                     expectedSlotId);

                AssertSlot(expectedSlot,
                           compareToSlot);
            }
        }

        private object GetSlotWithId(List <dynamic> list,
                                     int id)
        {
            return list.FirstOrDefault(slot => id == ( int ) ( slot [ "Id" ].Value ));
        }

        private dynamic CreatedExpectedJsonStringForList()
        {
            string json = "[" +
                          "{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}," +
                          "{\"Id\":2,\"DayId\":2,\"EndDateTime\":\"2015-07-01T09:15:00\",\"StartDateTime\":\"2015-07-01T09:00:00\",\"Status\":1}," +
                          "{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}," +
                          "{\"Id\":4,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:30:00\",\"StartDateTime\":\"2015-07-30T14:15:00\",\"Status\":1}," +
                          "{\"Id\":5,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:45:00\",\"StartDateTime\":\"2015-07-30T14:30:00\",\"Status\":2}," +
                          "{\"Id\":6,\"DayId\":4,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}" +
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

        private static void AssertSlot(dynamic expected,
                                       dynamic actual)
        {
            Console.WriteLine("Comparing slots with id {0} and {1}...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            Assert.True(expected [ "Id" ].Value == actual [ "Id" ].Value,
                        "Id");
            Assert.True(expected [ "DayId" ].Value == actual [ "DayId" ].Value,
                        "DayId");
            Assert.True(expected [ "EndDateTime" ].Value == actual [ "EndDateTime" ].Value,
                        "EndDateTime");
            Assert.True(expected [ "StartDateTime" ].Value == actual [ "StartDateTime" ].Value,
                        "StartDateTime");
            Assert.True(expected [ "Status" ].Value == actual [ "Status" ].Value,
                        "Status");
        }

        private static Browser CreateBrowser()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper,
                                      to => to.Accept("application/json"));
            return browser;
        }
    }
}