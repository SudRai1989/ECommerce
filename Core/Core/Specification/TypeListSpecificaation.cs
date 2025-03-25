﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class TypeListSpecificaation: BaseSpecification<Product, string>
    {
        public TypeListSpecificaation()
        {
            AddSelect(x => x.Type);
            ApplyDistinct();
        }
    }
}
