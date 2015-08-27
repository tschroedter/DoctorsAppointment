using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy;
using Newtonsoft.Json;

namespace MicroServices.Doctors.Nancy
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
            IEnumerable <IDoctorForResponse> doctors = m_InformationFinder.List();

            return AsJson(doctors);
        }

        public Response FindById(int id)
        {
            IDoctorForResponse doctor = m_InformationFinder.FindById(id);

            if ( doctor == null )
            {
                return HttpStatusCode.NotFound;
            }

            return AsJson(doctor);
        }

        public Response FindByLastName(string doctorLastName)
        {
            IEnumerable <IDoctorForResponse> doctors =
                m_InformationFinder.FindByLastName(doctorLastName)
                                   .ToArray();

            if ( !doctors.Any() )
            {
                return HttpStatusCode.NotFound;
            }

            if ( doctors.Count() > 1 )
            {
                return HttpStatusCode.Conflict;
            }

            return AsJson(doctors.First());
        }

        public Response Create(IDoctorForResponse doctor)
        {
            var created = m_InformationFinder.Create(doctor);

            if (created == null)
            {
                return HttpStatusCode.InternalServerError;
            }

            return AsJson(created);
        }

        public Response DeleteById(int id)
        {
            var doctor = m_InformationFinder.Delete(id);

            if (doctor == null)
            {
                return HttpStatusCode.NotFound;
            }

            return AsJson(doctor);
        }

        private Response AsJson(object instance)
        {
            Response response = JsonConvert.SerializeObject(instance);

            response.ContentType = "application/json";

            return response;
        }
    }
}