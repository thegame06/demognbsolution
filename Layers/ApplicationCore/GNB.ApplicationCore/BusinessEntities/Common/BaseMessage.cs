using System;

namespace GNB.ApplicationCore.BusinessEntities
{
    [Serializable]
    public class BaseMessage
    {
        public BaseMessage()
        {
            this.MessageType = MessageType.Object;
        }

        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        Object,
        String,
        Xml,
    }
}
