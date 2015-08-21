using System.Collections.Generic;
using JetBrains.Annotations;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy;
using Newtonsoft.Json;

namespace MicroServices.Slots.Nancy
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
            IEnumerable <ISlotForResponse> doctors = m_InformationFinder.List();

            return AsJson(doctors);
        }

        public Response FindById(int id)
        {
            ISlotForResponse doctor = m_InformationFinder.FindById(id);

            if ( doctor == null )
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