using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace MicroServices.Doctors.Nancy.Tests
{
    public static class XUnitDoctorsHelper
    {
        public static void AssertDoctorIgnoreId(dynamic expected,
                                                dynamic actual)
        {
            Console.WriteLine("Comparing doctors with id {0} and {1}...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            Assert.True(expected [ "LastName" ].Value == actual [ "LastName" ].Value,
                        "LastName");
            Assert.True(expected [ "FirstName" ].Value == actual [ "FirstName" ].Value,
                        "FirstName");
        }

        public static void AssertDoctor(dynamic expected,
                                        dynamic actual)
        {
            Console.WriteLine("Comparing doctors with id {0} and {1}...",
                              expected [ "Id" ].Value,
                              actual [ "Id" ].Value);

            Assert.True(expected [ "Id" ].Value == actual [ "Id" ].Value,
                        "Id");
            Assert.True(expected [ "LastName" ].Value == actual [ "LastName" ].Value,
                        "LastName");
            Assert.True(expected [ "FirstName" ].Value == actual [ "FirstName" ].Value,
                        "FirstName");
        }

        public static void AssertDoctors(dynamic expected,
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

                object compareToSlot = GetDoctorWithId(actualList,
                                                       expectedSlotId);

                AssertDoctor(expectedSlot,
                             compareToSlot);
            }
        }

        public static dynamic ToDynamic(string json)
        {
            dynamic data = JsonConvert.DeserializeObject(json);

            return data;
        }

        private static object GetDoctorWithId(IEnumerable <dynamic> list,
                                              int id)
        {
            return list.FirstOrDefault(slot => id == ( int ) slot [ "Id" ].Value);
        }
    }
}