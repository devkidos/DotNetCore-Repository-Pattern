using WBPOS.Data.Contracts;
using WBPOS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Data.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(WBPOSContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
