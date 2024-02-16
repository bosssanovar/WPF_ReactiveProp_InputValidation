using Entity;

namespace Repository
{
    public class InMemoryXXRepository : IXXRepository
    {
        public XXEntity XXEntity { get; private set; }

        public InMemoryXXRepository()
        {
            XXEntity = new XXEntity();
        }

        public XXEntity Load()
        {
            return XXEntity;
        }

        public void Save(XXEntity entity)
        {
            XXEntity = entity;
        }
    }
}
