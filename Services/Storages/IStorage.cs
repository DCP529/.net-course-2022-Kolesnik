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

        public void Update(T item);

        public void Delete(T item);
    }
}
