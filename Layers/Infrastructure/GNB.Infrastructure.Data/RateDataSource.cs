using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;
using GNB.ApplicationCore.Exception;
using GNB.Infrastructure.Data.Model;
using System;
using System.Linq;

namespace GNB.Infrastructure.Data
{
    [CurrentException(Class = "DataSourceException", NameSpace = "GNB.Infrastructure.Data", Assembly = "GNB.Infrastructure.Data")]
    public class RateDataSource : IRateDataSource
    {
        public IResult<string> Add(RatesData pMessageIn)
        {
            return this.UsingCatch(c =>
            {
                using (var context = new GNBDataDataContext())
                {

                    context.InsRates(Common.Serializer.SerializeBase64(pMessageIn));

                }

            });
        }


        public IResult<string> Get(Action<RatesData> pMessageOut)
        {
            return this.UsingCatch(c =>
             {
                 using (var context = new GNBDataDataContext())
                 {
                     var resultRates = context.GetRates().FirstOrDefault();

                     if (resultRates != null)
                     {
                         if (!string.IsNullOrEmpty(resultRates.Rates))
                         {
                             pMessageOut((RatesData)Common.Serializer.DeserializeBase64(resultRates.Rates));
                         }
                     }
                 }

             });
        }
    }
}
