using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IExpressionBuilder<TEntity, TProperty>
	{
		IMessageBuilder<TEntity> Custom(Expression<Func<TProperty, bool>> expression);
	}
}