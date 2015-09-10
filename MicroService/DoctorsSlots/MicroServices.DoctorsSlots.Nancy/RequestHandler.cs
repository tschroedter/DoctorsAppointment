using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DoctorsSlots.Nancy.Interfaces;
using Nancy;
using Newtonsoft.Json;
using Selkie.Windsor;

namespace MicroServices.DoctorsSlots.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class RequestHandler : IRequestHandler
    {
        private readonly IInformationFinder m_InformationFinder;

        public RequestHandler([NotNull] IInformationFinder informationFinder)
        {
            m_InformationFinder = informationFinder;
        }

        public Response List(int doctorId,
                             string date,
                             string status)
        {
            // todo Finder should return SlorForResponse
            IEnumerable <ISlot> list = m_InformationFinder.List(doctorId,
                                                                date,
                                                                status);

            IEnumerable <SlotForResponse> slots = list.Select(x => new SlotForResponse(x));

            return AsJson(slots);
        }

        private Response AsJson(object instance)
        {
            Response response = JsonConvert.SerializeObject(instance);

            response.ContentType = "application/json";

            return response;
        }
    }
}