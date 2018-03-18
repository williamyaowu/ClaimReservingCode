using ClaimReserving.Infrastructure.Persistant.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserving.Repository
{
    public class GeneralInfoRepository
        : IGeneralInfoRepository
    {
        private IPersistantSession _session;

        public GeneralInfoRepository(IPersistantSession session)
        {
            this._session = session;
        }

        public bool Write(string info)
        {
            try
            {
                    _session.Write(info);
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }
    }
}
