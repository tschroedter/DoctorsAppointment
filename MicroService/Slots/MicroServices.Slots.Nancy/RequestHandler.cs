using System.Collections.Generic;
using JetBrains.Annotations;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy;
using Newtonsoft.Json;
using Selkie.Windsor;

namespace MicroServices.Slots.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class RequestHandler : IRequestHandler
    {
        private readonly IInformationFinder m_InformationFinder;

        public RequestHandler([NotNull] IInformationFinder informationFinder)
        {
            m_InformationFinder = informationFinder;
        }

        public Response List()
        {
            IEnumerable <ISlotForResponse> slots = m_InformationFinder.List();

            return AsJson(slots);
        }

        public Response FindById(int id)
        {
            ISlotForResponse slot = m_InformationFinder.FindById(id);

            if ( slot == null )
            {
                return HttpStatusCode.NotFound;
            }

            return AsJson(slot);
        }

        public Response Save(ISlotForResponse slot)
        {
            ISlotForResponse saved = m_InformationFinder.Save(slot);

            return AsJson(saved);
        }

        public Response DeleteById(int id)
        {
            ISlotForResponse slot = m_InformationFinder.Delete(id);

            if ( slot == null )
            {
                return HttpStatusCode.NotFound;
            }

            return AsJson(slot);
        }

        private Response AsJson(object instance)
        {
            Response response = JsonConvert.SerializeObject(instance);

            response.ContentType = "application/json";

            return response;
        }
    }
}