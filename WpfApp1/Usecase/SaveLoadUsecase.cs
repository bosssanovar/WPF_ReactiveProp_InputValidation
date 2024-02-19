using Entity;
using Repository;

namespace Usecase
{
    public class SaveLoadUsecase(IXXRepository repository)
    {
        private readonly IXXRepository repository = repository;

        public event Action? OnSomeEnumChanged;

        public void Save(XXEntity entity)
        {
            entity.OnSomeEnumChanged -= Entity_OnSomeEnumChanged;
            repository.Save(entity);
        }

        public XXEntity Load()
        {
            var entity = repository.Load();
            entity.OnSomeEnumChanged += Entity_OnSomeEnumChanged;

            return entity;
        }

        private void Entity_OnSomeEnumChanged()
        {
            OnSomeEnumChanged?.Invoke();
        }
    }
}
