namespace VezeetaApi.Repository
{
    public interface IBaseRepo<T>where T : class
    {
        List<T> GettAll();
        T GettById(int id);
        void ADD(T item);
        void UPDATE(T item, int id);
        void DELETE(string id);
    }
}
