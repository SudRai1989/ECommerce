﻿using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        protected BaseSpecification(): this(null) { }
        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool IsDistinct { get; private set; }

        protected void AddOrderBy(Expression<Func<T, Object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, Object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }

        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }
    }

    // changes for non generic result
    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) :
        BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        protected BaseSpecification() : this(null!) { }
        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, TResult>> SelectExpression)
        {
            Select = SelectExpression;
        }
    }
}
