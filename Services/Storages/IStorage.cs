using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Models.ModelsDb;

namespace Services
{
    public interface IStorage<T>
    {
        public void Add(T item);

        public void Update(Guid id, T item);

        public void Delete(Guid item);
    }
}
