
using VezeetaApi.Data;

namespace VezeetaApi.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
        }

        public void ADD(T item)
        {
            _context.Add(item);
            _context.SaveChanges();

        }

        public void DELETE(string id)
        {
            _context.Remove(_context.Set<T>());
            _context.SaveChanges();
        }

        public List<T> GettAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GettById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void UPDATE(T item, int id)
        {
            GettById(id);
            if (item == null)
            {

                throw new ArgumentException($"Entity with ID {id} not found.");

            }


            _context.SaveChanges();
        }
    }
}
