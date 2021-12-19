using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using OE.SigMod.Models;

namespace OE.SigMod.Repository
{
    public class SigModRepository : ISigModRepository, IService
    {
        private readonly SigModContext _db;

        public SigModRepository(SigModContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.SigMod> GetSigMods(int ModuleId)
        {
            return _db.SigMod.Where(item => item.ModuleId == ModuleId);
        }

        public Models.SigMod GetSigMod(int SigModId)
        {
            return GetSigMod(SigModId, true);
        }

        public Models.SigMod GetSigMod(int SigModId, bool tracking)
        {
            if (tracking)
            {
                return _db.SigMod.Find(SigModId);
            }
            else
            {
                return _db.SigMod.AsNoTracking().FirstOrDefault(item => item.SigModId == SigModId);
            }
        }

        public Models.SigMod AddSigMod(Models.SigMod SigMod)
        {
            _db.SigMod.Add(SigMod);
            _db.SaveChanges();
            return SigMod;
        }

        public Models.SigMod UpdateSigMod(Models.SigMod SigMod)
        {
            _db.Entry(SigMod).State = EntityState.Modified;
            _db.SaveChanges();
            return SigMod;
        }

        public void DeleteSigMod(int SigModId)
        {
            Models.SigMod SigMod = _db.SigMod.Find(SigModId);
            _db.SigMod.Remove(SigMod);
            _db.SaveChanges();
        }
    }
}
