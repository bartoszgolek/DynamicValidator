using System;

namespace DynamicValidation.Core
{
	public interface IRuleBuilder<TEntity, out TProperty>
	{
		ValidatorBuilder<TEntity> Custom(Func<TProperty, bool> rule, string message);
	}
}