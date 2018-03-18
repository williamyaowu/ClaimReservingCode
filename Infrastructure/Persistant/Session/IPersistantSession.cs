using System;
using System.Collections.Generic;

namespace ClaimReserving.Infrastructure.Persistant.Session
{
    public interface IPersistantSession
         : IDisposable
    {
        IList<object> ReadAll();

        void Write(object record);
    }
}
