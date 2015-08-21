﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DoctorsSlots.Nancy.Interfaces;
using Nancy;
using Newtonsoft.Json;

namespace MicroServices.DoctorsSlots.Nancy
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IInformationFinder m_InformationFinder;

        public RequestHandler([NotNull] IInformationFinder informationFinder)
        {
            m_InformationFinder = informationFinder;
        }

        public Response List(string doctorLastName,
                             string date,
                             string status)
        {
            // todo Finder should return SlorForResponse
            IEnumerable <ISlot> list = m_InformationFinder.List(doctorLastName,
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