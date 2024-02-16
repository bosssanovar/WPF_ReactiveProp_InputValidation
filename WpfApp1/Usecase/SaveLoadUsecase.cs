using Entity;
using Repository;

namespace Usecase
{
    public class SaveLoadUsecase
    {
        IXXRepository repository;

        public SaveLoadUsecase(IXXRepository repository)
        {
            this.repository = repository;
        }

        public void Save(XXEntity entity)
        {
            repository.Save(entity);
        }

        public XXEntity Load()
        {
            return repository.Load();
        }
    }
}
