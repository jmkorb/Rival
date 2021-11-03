using Rival.Data;
using Rival.Models.Courts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rival.Services.CourtServices
{
    public class CourtService
    {
        public CourtService() { }
        public bool CreateCourt(CourtCreate model)
        {
            var entity = new Court()
            {
               Location = model.Location,
               Condition = model.Condition
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Courts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool EditCourt(CourtDetail model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Courts
                        .Single(e => e.Id == model.CourtId);

                entity.Location = model.Location;
                entity.Condition = model.Condition;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCourt(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Courts
                        .Single(e => e.Id == id);

                ctx.Courts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CourtDetail> GetCourts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Courts
                        .Select(
                            e =>
                                new CourtDetail
                                {
                                    CourtId = e.Id,
                                    Location = e.Location,
                                    Condition = e.Condition
                                }
                        );

                return query.ToArray();
            }
        }
        public CourtDetail GetCourtById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Courts
                        .Single(e => e.Id == id);

                return new CourtDetail
                {
                    CourtId = entity.Id,
                    Location = entity.Location,
                    Condition = entity.Condition
                };
            }
        }
    }
}
}
