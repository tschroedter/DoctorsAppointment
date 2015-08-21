using System;
using System.Data.Entity.Migrations;
using MicroServices.DataAccess.DoctorsSlots.Contexts;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration <DoctorsSlotsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DoctorsSlotsContext context)
        {
            SeedDoctors(context);
        }

        private void SeedDoctors(DoctorsSlotsContext context)
        {
            CreateMaryMiller(context);
            CreateWillSmith(context);
        }

        private static void CreateMaryMiller(DoctorsSlotsContext context)
        {
            var doctor = new Doctor
                         {
                             FirstName = "Mary",
                             LastName = "Miller"
                         };

            var dayOne = new Day
                         {
                             Date = DateTime.Parse("2015-06-30"),
                             Doctor = doctor
                         };

            var slotOne = new Slot
                          {
                              StartDateTime = DateTime.Parse("2015-06-30 09:00:00"),
                              EndDateTime = DateTime.Parse("2015-06-30 09:15:00"),
                              Status = SlotStatus.Open,
                              Day = dayOne
                          };


            var dayTwo = new Day
                         {
                             Date = DateTime.Parse("2015-07-01"),
                             Doctor = doctor
                         };

            var slotTwo = new Slot
                          {
                              StartDateTime = DateTime.Parse("2015-07-01 09:00:00"),
                              EndDateTime = DateTime.Parse("2015-07-01 09:15:00"),
                              Status = SlotStatus.Open,
                              Day = dayTwo
                          };

            dayOne.Slots.Add(slotOne);
            doctor.Days.Add(dayOne);

            dayTwo.Slots.Add(slotTwo);
            doctor.Days.Add(dayTwo);

            context.Doctors.AddOrUpdate(doctor);
        }

        private static void CreateWillSmith(DoctorsSlotsContext context)
        {
            var doctor = new Doctor
                         {
                             FirstName = "Will",
                             LastName = "Smith"
                         };

            var dayOne = new Day
                         {
                             Date = DateTime.Parse("2015-07-30"),
                             Doctor = doctor
                         };

            var slotOne = new Slot
                          {
                              StartDateTime = DateTime.Parse("2015-07-30 14:00:00"),
                              EndDateTime = DateTime.Parse("2015-07-30 14:15:00"),
                              Status = SlotStatus.Unknown,
                              Day = dayOne
                          };

            var slotTwo = new Slot
                          {
                              StartDateTime = DateTime.Parse("2015-07-30 14:15:00"),
                              EndDateTime = DateTime.Parse("2015-07-30 14:30:00"),
                              Status = SlotStatus.Open,
                              Day = dayOne
                          };

            var slotThree = new Slot
                            {
                                StartDateTime = DateTime.Parse("2015-07-30 14:30:00"),
                                EndDateTime = DateTime.Parse("2015-07-30 14:45:00"),
                                Status = SlotStatus.Booked,
                                Day = dayOne
                            };

            dayOne.Slots.Add(slotOne);
            dayOne.Slots.Add(slotTwo);
            dayOne.Slots.Add(slotThree);
            doctor.Days.Add(dayOne);

            var dayTwo = new Day
                         {
                             Date = DateTime.Parse("2015-06-30"),
                             Doctor = doctor
                         };

            var dayTwoSlot = new Slot
                             {
                                 StartDateTime = DateTime.Parse("2015-06-30 09:00:00"),
                                 EndDateTime = DateTime.Parse("2015-06-30 09:15:00"),
                                 Status = SlotStatus.Open,
                                 Day = dayTwo
                             };

            dayTwo.Slots.Add(dayTwoSlot);
            doctor.Days.Add(dayTwo);

            context.Doctors.AddOrUpdate(doctor);
        }
    }
}