using GNB.ApplicationCore.Interfaces;
using System;

namespace GNB.ApplicationCore.BusinessEntities
{
    public class Result : EventArgs, IResult
    {
        public string StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public bool IsSuccessStatusCode { get; private set; }

        public bool Successfully { get { return IsSuccessStatusCode && Exception == null; } }

        public System.Exception Exception { get; private set; }

        public Result(System.Exception exception, params object[] args)
        {
            if (args != null)
            {
                switch (args.Length)
                {
                    case 1:
                        StatusDescription = args[0].ToString();
                        StatusCode = "90001";
                        IsSuccessStatusCode = false;
                        break;
                    case 2:
                        StatusDescription = args[1].ToString();
                        StatusCode = string.Format("{0:00000}", args[0]);
                        IsSuccessStatusCode = false;
                        break;
                    case 3:
                        StatusDescription = args[1].ToString() + args[2].ToString();
                        StatusCode = string.Format("{0:00000}", args[0]);
                        IsSuccessStatusCode = false;
                        break;
                    default:
                        StatusDescription = "Internal error.";
                        StatusCode = "90001";
                        IsSuccessStatusCode = false;
                        break;
                }
            }
            else
            {
                StatusCode = "00000";
                StatusDescription = "OK.";
                IsSuccessStatusCode = true;
            }

            Exception = exception;
        }

        public string GetString()
        {
            return string.Format("Successfully:{0}, StatusDescription:{1}", this.Successfully.ToString(), StatusDescription);
        }
    }
}
