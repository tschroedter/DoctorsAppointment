using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Days.Nancy.Interfaces;
using Nancy;
using Newtonsoft.Json;

namespace MicroServices.Days.Nancy
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IInformationFinder m_InformationFinder;

        public RequestHandler([NotNull] IInformationFinder informationFinder)
        {
            m_InformationFinder = informationFinder;
        }

        public Response List()
        {
            IEnumerable <IDayForResponse> doctors = m_InformationFinder.List();

            return AsJson(doctors);
        }

        public Response FindById(int id)
        {
            IDayForResponse doctor = m_InformationFinder.FindById(id);

            if ( doctor == null )
            {
                return HttpStatusCode.NotFound;
            }

            return AsJson(doctor);
        }

        // todo check if we have o rename find to FindForDateAndDoctor, check others
        public Response Find(string date,
                             string doctorId)
        {
            IEnumerable <IDayForResponse> list = m_InformationFinder.ListForDateAndDoctorId(date,
                                                                                            doctorId);

            return AsJson(list);
        }

        public Response FindByDate(string date)
        {
            IEnumerable <IDayForResponse> doctors = m_InformationFinder.ListForDate(date);

            return AsJson(doctors);
        }

        public Response FindByDoctorId(int doctorId)
        {
            IEnumerable <IDayForResponse> days = m_InformationFinder.FindByDoctorId(doctorId)
                                                                    .ToArray();

            if ( !days.Any() )
            {
                return HttpStatusCode.NotFound;
            }

            return AsJson(days);
        }

        private Response AsJson(object instance)
        {
            Response response = JsonConvert.SerializeObject(instance);

            response.ContentType = "application/json";

            return response;
        }
    }
}