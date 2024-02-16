using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    public class InitUsecase
    {
        IXXRepository repository;

        public InitUsecase(IXXRepository repository)
        {
            this.repository = repository;
        }

        public void Init()
        {
            var entity = repository.Load();
            entity.Init();
            repository.Save(entity);
        }
    }
}
