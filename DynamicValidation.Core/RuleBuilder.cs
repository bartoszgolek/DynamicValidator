using System;

namespace DynamicValidation.Core
{
	public class RuleBuilder<TEntity, TProperty> : IRuleBuilder<TEntity, TProperty>
	{
		private readonly ValidatorBuilder<TEntity> validatorBuilder;
		private readonly Func<TEntity, TProperty> getValue;

		public RuleBuilder(ValidatorBuilder<TEntity> validatorBuilder, Func<TEntity, TProperty> getValue)
		{
			this.validatorBuilder = validatorBuilder;
			this.getValue = getValue;
		}

		public ValidatorBuilder<TEntity> Custom(Func<TProperty, bool> rule, string message)
		{
			validatorBuilder.WithRule(getValue, rule, message);
			return validatorBuilder;
		}
	}
}