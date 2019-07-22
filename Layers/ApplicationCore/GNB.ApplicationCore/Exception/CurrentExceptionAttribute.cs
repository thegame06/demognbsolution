using System;

namespace GNB.ApplicationCore.Exception
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class CurrentExceptionAttribute : Attribute
    {
        public string Assembly { get; set; }

        public string NameSpace { get; set; }

        public string Class { get; set; }

        public bool Enabled { get; set; }

        public CurrentExceptionAttribute()
        {
            Enabled = true;
        }

        public Type GetOverride()
        {
            Type type = null;

            if (Enabled)
            {
                if (NameSpace != string.Empty && Assembly != string.Empty)
                {
                    type = Type.GetType(string.Format("{0}.{1}, {2}", NameSpace, Class, Assembly));
                }
            }

            return type;
        }
    }
}
