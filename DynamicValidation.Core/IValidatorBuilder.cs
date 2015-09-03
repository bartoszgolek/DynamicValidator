using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IValidatorBuilder<TEntity>
	{
		IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue);
	}
}