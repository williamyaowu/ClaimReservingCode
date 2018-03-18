using System;
using System.Collections.Generic;
using System.Linq;
using ClaimReserving.Models;
using ClaimReserving.Infrastructure.Persistant.Session;

namespace ClaimReserving.Repository
{
    public class CumlativeClaimDataRepository
        : ICumlativeClaimDataRepository
    {
        private IPersistantSession _session;

        public CumlativeClaimDataRepository(IPersistantSession session)
        {
            this._session = session;
        }

        public bool Save(IList<CumulativeClaimData> records)
        {
           try
            {
                foreach( var re in records)
                {
                    _session.Write(this.serialization(re));
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return true;
        }

        private string serialization(CumulativeClaimData record)
        {
            var data = record.Data.OrderBy(p => p.OriginalYear).ThenBy(p => p.DevelopmentYearNumber).Select(p=>p.Amount);

            return string.Format("{0}; {1}", record.Product, string.Join("; ", data));
        }
    }
}
