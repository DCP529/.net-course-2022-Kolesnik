using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    interface IStorage
    {
        public void Add(Client client);

        public void Update();

        public void Delete();
    }
}
