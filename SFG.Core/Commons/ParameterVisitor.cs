using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace SFG.Core.Commons
{
	public class ParameterVisitor : ExpressionVisitor
	{
		private readonly ReadOnlyCollection<ParameterExpression> from, to;

		public ParameterVisitor(ReadOnlyCollection<ParameterExpression> from, ReadOnlyCollection<ParameterExpression> to)
		{
			this.from = from;
			this.to = to;
		}

        protected override Expression VisitParameter(ParameterExpression node)
        {
			for(int i=0; i<from.Count; i++)
			{
				if (node == from[i])
					return to[i];
			}
            return base.VisitParameter(node);
        }
    }
}

