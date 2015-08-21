using Nancy;
using Nancy.Testing;
using Xunit;

namespace MicroServices.DoctorsSlots.Tests.Integration.Nancy.Nancy
{
    public sealed class DoctorsSlotsModuleTests
    {
        [Fact]
        public void Should_return_status_OK_when_doctor_with_lastname_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller/slots",
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
        public void Should_return_JSON_when_doctor_with_lastname_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller/slots",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal("application/json",
                         result.ContentType);
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists_and_date_exists()
        {
            // Given
            string expected = CreateExpectedStringForMillerSlotsWithDate();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Miller/slots",
                                                 with =>
                                                 {
                                                     with.Query("date",
                                                                "2015-06-30");
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists_and_date_and_status()
        {
            // Given
            string expected = CreateExpectedStringForSmithSlotsWithDateAndStatusOpen();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Smith/slots",
                                                 with =>
                                                 {
                                                     with.Query("date",
                                                                "2015-07-30");
                                                     with.Query("status",
                                                                "open");
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        private string CreateExpectedStringForMillerSlotsWithDate()
        {
            return "[" +
                   "{\"Id\":1,\"DayId\":1,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}" +
                   "]";
        }

        /*
         * [{"Status":1,"StartDateTime":"2015-06-30T09:00:00","EndDateTime":"2015-06-30T09:15:00","Id":1},{"Status":1,"StartDateTime":"2015-07-01T09:00:00","EndDateTime":"2015-07-01T09:15:00","Id":2}]
         */

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists_and_status_open()
        {
            // Given
            string expected = CreateExpectedStringForSmithSlotsWithStatusOpen();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Smith/slots",
                                                 with =>
                                                 {
                                                     with.Query("status",
                                                                "open");
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists_and_status_unnknown()
        {
            // Given
            string expected = CreateExpectedStringForSmithSlotsWithStatusUnknown();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Smith/slots",
                                                 with =>
                                                 {
                                                     with.Query("status",
                                                                "unknown");
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists_and_status_booked()
        {
            // Given
            string expected = CreateExpectedStringForSmithSlotsWithStatusBooked();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Smith/slots",
                                                 with =>
                                                 {
                                                     with.Query("status",
                                                                "booked");
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        [Fact]
        public void Should_return_JSON_string_when_doctor_with_lastname_exists()
        {
            // Given
            string expected = CreateExpectedStringForSmithSlots();
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Smith/slots",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(expected,
                         result.Body.AsString());
        }

        private string CreateExpectedStringForSmithSlots()
        {
            return "[" +
                   "{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}," +
                   "{\"Id\":4,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:30:00\",\"StartDateTime\":\"2015-07-30T14:15:00\",\"Status\":1}," +
                   "{\"Id\":5,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:45:00\",\"StartDateTime\":\"2015-07-30T14:30:00\",\"Status\":2}," +
                   "{\"Id\":6,\"DayId\":4,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}" +
                   "]";
        }

        private string CreateExpectedStringForSmithSlotsWithStatusUnknown()
        {
            return "[" +
                   "{\"Id\":3,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:15:00\",\"StartDateTime\":\"2015-07-30T14:00:00\",\"Status\":0}" +
                   "]";
        }

        private string CreateExpectedStringForSmithSlotsWithStatusBooked()
        {
            return "[" +
                   "{\"Id\":5,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:45:00\",\"StartDateTime\":\"2015-07-30T14:30:00\",\"Status\":2}" +
                   "]";
        }

        private string CreateExpectedStringForSmithSlotsWithDateAndStatusOpen()
        {
            return "[" +
                   "{\"Id\":4,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:30:00\",\"StartDateTime\":\"2015-07-30T14:15:00\",\"Status\":1}" +
                   "]";
        }

        private string CreateExpectedStringForSmithSlotsWithStatusOpen()
        {
            return "[" +
                   "{\"Id\":4,\"DayId\":3,\"EndDateTime\":\"2015-07-30T14:30:00\",\"StartDateTime\":\"2015-07-30T14:15:00\",\"Status\":1}," +
                   "{\"Id\":6,\"DayId\":4,\"EndDateTime\":\"2015-06-30T09:15:00\",\"StartDateTime\":\"2015-06-30T09:00:00\",\"Status\":1}" +
                   "]";
        }

        [Fact]
        public void Should_return_status_OK_when_doctor_lastname_doesnot_exists()
        {
            // Given
            Browser browser = CreateBrowser();

            // When
            BrowserResponse result = browser.Get("/doctors/Unknown/slots",
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.Equal(HttpStatusCode.OK,
                         result.StatusCode);
        }
    }
}