﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common.Crud
{
    public interface ICrudService<T> : IEnumerable<T>
    {
        void Create(T element);
        T Read(Guid Id);
        IEnumerable<T> ReadAll();
        void Update(T element);
        void Remove(T element);
        Task Save(string filePath);
        Task Load(string filePath);
    }

}

