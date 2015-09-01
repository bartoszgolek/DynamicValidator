using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IRuleBuilder<TEntity, TProperty>
	{
		IValidatorBuilderWithMessageBuilder<TEntity, TProperty> Custom(Expression<Func<TProperty, bool>> rule);
	}
}