using VezeetaApi.Data;
using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Repository.SpacializationRepo
{
    public class SpecializationRepo : ISpecializationRepo
    {
        private readonly AppDbContext _context;

        public SpecializationRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Add(SpecializationDto item)
        {
            var specializationwithId = _context.Specializations.
                FirstOrDefault(d => d.SpecializationId == item.SpecializationId);
            if(specializationwithId != null)
            {
                var specialization = new Specialization
                {
                    SpecializationId = item.SpecializationId,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                };
                _context.Specializations.Add(specialization);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Specialization not found");
            }
        }

        public Specialization GetById(int id)
        {
            var specialization = _context.Specializations
                .FirstOrDefault(d => d.SpecializationId ==id);
            if(specialization!=null)
            {
                return specialization;
            }
            else
            {
                throw new ArgumentException("Specialization not found");
            }

        }

        
    }
}
