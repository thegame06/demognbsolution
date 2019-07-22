using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Exception;
using GNB.ApplicationCore.Interfaces;
using System;
using System.ServiceModel;

namespace GNB.Infrastructure.ServiceAgents.Common
{
    public static class ServiceClientWrapperExt
    {
        public static IResult<string> Using<T>(this T pClient, Action<T> pWork)
            where T : ICommunicationObject
        {
            IResult<string> resultado = null;

            try
            {
                try
                {
                    pWork(pClient);

                    resultado = new Result<string>("OK");
                }
                catch (CommunicationException exCom)
                {
                    pClient.Abort();

                    throw new CustomException<string>(null, 90003, string.Format("{0} \n {1} \n {2} \n", pWork.Method.Module, pWork.Target.ToString(), pWork.Method.Name), exCom, pClient.GetTypeCurrentException());
                }
                catch (System.TimeoutException exTO)
                {
                    pClient.Abort();

                    throw new CustomException<string>(null, 90002, string.Format("{0} \n {1} \n {2} \n", pWork.Method.Module, pWork.Target.ToString(), pWork.Method.Name), exTO, pClient.GetTypeCurrentException());
                }
                catch (System.Exception ex)
                {
                    pClient.Abort();

                    throw new CustomException<string>(null, 90001, string.Format("{0} \n {1} \n {2} \n", pWork.Method.Module, pWork.Target.ToString(), pWork.Method.Name), ex, pClient.GetTypeCurrentException());
                }
            }
            catch (CustomException<string> exception)
            {
                switch (exception.Result.StatusCode)
                {
                    case "90001":
                        exception.Result.StatusDescription = "Internal Error.";
                        break;

                    case "90002":
                        exception.Result.StatusDescription = "Timeout Error.";
                        break;

                    case "90003":
                        exception.Result.StatusDescription = "Communication Error.";
                        break;
                }

                resultado = exception.Result;
            }

            return resultado;
        }

    }
}
