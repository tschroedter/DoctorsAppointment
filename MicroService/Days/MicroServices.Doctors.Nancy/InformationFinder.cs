using System;
using System.Collections.Generic;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Days.Nancy.Interfaces;

namespace MicroServices.Days.Nancy
{
    public class InformationFinder : IInformationFinder
    {
        private readonly IDaysRepository m_Repository;

        public InformationFinder(IDaysRepository repository)
        {
            m_Repository = repository;
        }

        public IEnumerable <IDayForResponse> FindByDoctorId(int doctorId)
        {
            IEnumerable <IDayForResponse> days = m_Repository.FindByDoctorId(doctorId)
                                                             .Select(day => new DayForResponse(day))
                                                             .ToArray();

            return days;
        }

        public IDayForResponse FindById(int id)
        {
            IDay day = m_Repository.FindById(id);

            if ( day == null )
            {
                return null;
            }

            return new DayForResponse(day);
        }

        public IEnumerable <IDayForResponse> List()
        {
            IEnumerable <IDay> all = m_Repository.All;

            DayForResponse[] days = ToDayForResponses(all);

            return days;
        }

        public IEnumerable <IDayForResponse> ListForDate(string date)
        {
            DateTime dateTime;

            if ( DateTime.TryParse(date,
                                   out dateTime) )
            {
                IEnumerable <IDay> all = m_Repository.FindByDate(dateTime);

                DayForResponse[] days = ToDayForResponses(all);

                return days;
            }

            return new IDayForResponse[0];
        }

        public IEnumerable <IDayForResponse> ListForDateAndDoctorId(string date,
                                                                    int doctorId)
        {
            IEnumerable <IDay> all = m_Repository.All;

            all = FilterByDate(all,
                               date);

            all = FilterByDoctorId(all,
                                   doctorId);

            DayForResponse[] days = ToDayForResponses(all);

            return days;
        }

        private IEnumerable <IDay> FilterByDate(IEnumerable <IDay> all,
                                                 string date)
        {
            if ( !string.IsNullOrEmpty(date) )
            {
                DateTime dateTime;

                if ( DateTime.TryParse(date,
                                       out dateTime) )
                {
                    all = all.Where(x => x.Date == dateTime);
                }
            }
            return all;
        }

        internal IEnumerable <IDay> FilterByDoctorId(IEnumerable <IDay> all,
                                                     int doctorId)
        {
            all = all.Where(x => x.DoctorId == doctorId);

            return all;
        }

        private static DayForResponse[] ToDayForResponses(IEnumerable <IDay> all)
        {
            DayForResponse[] days = all.Select(day => new DayForResponse(day))
                                       .ToArray();
            return days;
        }
    }
}