﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MicroServices.Days.Tests.Integration.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class DaysModuleTests
    {
        [Fact]
        public void Should_return_status_OK_when_slot_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/days/1",
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
            AssertSlot(expected,
                       actual);
        }

        [Fact]
        public void Should_return_status_NotFound_when_slot_id_doesnot_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/days/-1",
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
            BrowserResponse result = browser.Get("/days/1",
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
            BrowserResponse result = browser.Get("/days/",
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
            BrowserResponse result = browser.Get("/days/",
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

                AssertSlot(expectedSlot,
                           compareToSlot);
            }
        }

        private object GetDayWithId(List <dynamic> list,
                                    int id)
        {
            return list.FirstOrDefault(slot => id == ( int ) ( slot [ "Id" ].Value ));
        }

        private dynamic CreatedExpectedJsonStringForList()
        {
            string json = "[" +
                          "{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":1,\"Id\":1},{\"Date\":\"2015-07-01T00:00:00\",\"DoctorId\":1,\"Id\":2}," +
                          "{\"Date\":\"2015-07-30T00:00:00\",\"DoctorId\":2,\"Id\":3},{\"Date\":\"2015-06-30T00:00:00\",\"DoctorId\":2,\"Id\":4}" +
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

        private static void AssertSlot(dynamic expected,
                                       dynamic actual)
        {
            Console.WriteLine("Comparing days with id {0} and {1}...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            Assert.True(expected [ "Id" ].Value == actual [ "Id" ].Value,
                        "Id");
            Assert.True(expected [ "Date" ].Value == actual [ "Date" ].Value,
                        "DayId");
            Assert.True(expected [ "DoctorId" ].Value == actual [ "DoctorId" ].Value,
                        "EndDateTime");
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