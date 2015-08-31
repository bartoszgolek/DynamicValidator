using System;

namespace Validate
{
	public interface IRuleBuilder<TEntity, out TProperty>
	{
		ValidatorBuilder<TEntity> Custom(Func<TProperty, bool> rule, string message);
	}
}