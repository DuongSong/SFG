using System;
using System.Linq.Expressions;

namespace SFG.Core.Commons
{
	public static class ConditionBuilder
	{
		public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> condition)
		{
			return condition;
		}

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> conditionFirst, Expression<Func<T, bool>> conditionSecond)
		{
			var secondBody = new ParameterVisitor(conditionSecond.Parameters, conditionFirst.Parameters).Visit(conditionSecond.Body);

			return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(conditionFirst.Body, secondBody), conditionFirst.Parameters);
		}
	}
}

