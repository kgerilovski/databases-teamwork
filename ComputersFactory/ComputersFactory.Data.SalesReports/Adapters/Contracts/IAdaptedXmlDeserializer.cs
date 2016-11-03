﻿using System.Collections.Generic;

using ComputersFactory.Data.SalesReports.XmlDeserializers.Contracts;

namespace ComputersFactory.Data.SalesReports.Adapters.Contracts
{
    public interface IAdaptedXmlDeserializer
    {
        IList<TModel> DeserializeXmlToIListOf<TModel>(string fileName, string rootElement);
    }
}
