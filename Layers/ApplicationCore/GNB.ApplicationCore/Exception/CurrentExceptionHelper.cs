using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;
using System;
using System.Linq;

namespace GNB.ApplicationCore.Exception
{
    public static class CurrentExceptionHelper
    {
        public static Type GetTypeCurrentException(this object pObj)
        {
            Type type = null;

            var attributes = pObj.GetType()
               .GetCustomAttributes(typeof(CurrentExceptionAttribute), true)
               .Cast<CurrentExceptionAttribute>();

            // Ignore any types where enabled=false.
            if (attributes.Any(a => a.Enabled))
            {
                foreach (var item in attributes)
                {
                    type = item.GetOverride();
                }
            }

            return type;
        }

        public static IResult<string> UsingCatch_Func<T>(this T client, Func<T, IResult<string>> work)
        {
            IResult<string> resultado = null;

            try
            {
                try
                {
                    return work(client);
                }
                catch (System.Exception ex)
                {
                    var target = work.Target != null ? work.Target.ToString() : string.Empty;
                    throw new CustomException<string>(null, 90001, string.Format("{0} \n {1} \n {2} \n", work.Method.Module, target, work.Method.Name), ex, client.GetTypeCurrentException());
                }
            }
            catch (CustomException<string> exception)
            {
                exception.Result.StatusDescription = "Internal Error.";

                resultado = exception.Result;
            }

            return resultado;
        }


        public static IResult<string> UsingCatch<T>(this T client, Action<T> work)
              where T : class
        {
            IResult<string> resultado = null;

            try
            {
                try
                {
                    work(client);

                    resultado = new Result<string>("OK");
                }
                catch (System.Exception ex)
                {
                    throw new CustomException<string>(null, 90001, string.Format("{0} \n {1} \n {2} \n", work.Method.Module, work.Target.ToString(), work.Method.Name), ex, client.GetTypeCurrentException());
                }
            }
            catch (CustomException<string> exception)
            {
                exception.Result.StatusDescription = "Internal Error.";

                resultado = exception.Result;
            }

            return resultado;
        }
    }
}
