using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mvc5IdentityExample.Data.EntityFramework;
using Mvc5IdentityExample.Data.EntityFramework.Repositories;
using Mvc5IdentityExample.Domain.Entities;
using NUnit.Framework;

namespace Data.Tests
{

    class SimpleSpecification<T> : ISpecification<T> where T : class
    {
        string _value;

        public SimpleSpecification(string value)
        {
            _value = value;
        }

        public string Property { get; set; }
        public Func<T, string> Property2 { get; set; }


        public Expression<Func<T, bool>> GetExpression<TValue>(Expression<Func<T, TValue>> propertyExpr)
        {
            ParameterExpression paramExp = propertyExpr.Parameters.Single();
            var expression = propertyExpr.Body as MemberExpression;

            BinaryExpression binaryExpression = Expression.Equal(Expression.Constant(_value, typeof(TValue)), expression);

            return Expression.Lambda<Func<T, bool>>(binaryExpression, paramExp);
        }


        public Expression<Func<T, bool>> GetExpression()
        {
            ParameterExpression paramExp = Expression.Parameter(typeof(T), "x");
            MemberExpression memberExp = Expression.Property(paramExp, Property);
            var p = ToLambda<T>(Property);
            // Expression<Func<T, bool>> restExpression = x => _value == Expression.Lambda<Func<T,string>>(memberExp,paramExp).Compile();
            BinaryExpression binaryExpression = Expression.Equal(Expression.Constant(_value), memberExp);

            return Expression.Lambda<Func<T, bool>>(binaryExpression, paramExp);
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

    }
    public static class QueryableExtensions
    {
        public static Expression<Func<TElement, bool>> BuildOrExpression<TElement, TValue>(
       Expression<Func<TElement, TValue>> valueSelector,
       IEnumerable<TValue> values
   )
        {
            if (null == valueSelector)
                throw new ArgumentNullException("valueSelector");

            if (null == values)
                throw new ArgumentNullException("values");

            ParameterExpression p = valueSelector.Parameters.Single();


            if (!values.Any())
                return e => false;


            var equals = values.Select(value =>
                (Expression)Expression.Equal(
                     valueSelector.Body,
                     Expression.Constant(
                         value,
                         typeof(TValue)
                     )
                )
            );
            var body = equals.Aggregate<Expression>(
                     (accumulate, equal) => Expression.Or(accumulate, equal)
             );

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        //public static IQueryable<T> Find<T>(this IQueryable<T> source, string propertyName)
        //{
        //    return source.Where()
        //} 
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
    [TestFixture]
    public class FilterTests
    {
        private UnitOfWork _unitOfWork;
        private UserRepository _repository;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new UnitOfWork(
                @"Data Source=DESKTOP-183ADHK\\SQLEXPRESS;Initial Catalog=Mvc5IdentityExample;Integrated Security=True");
            _repository = new UserRepository(_unitOfWork);
        }
        [Test]
        public void TestFilters()
        {
            IFilter filter;

            //var dbSet = _unitOfWork.Context.Users.ToList();
            _repository.Add(new User { UserName = "First"});
            _repository.Add(new User { UserName = "Second"});
            _unitOfWork.SaveChanges();

            var users = _repository.GetAll();

            Assert.That(users.Count, Is.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Dispose();
        }
    }
}
