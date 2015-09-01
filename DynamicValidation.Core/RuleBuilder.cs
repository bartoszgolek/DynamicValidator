using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public class RuleBuilder<TEntity, TProperty> : IRuleBuilder<TEntity, TProperty>
	{
		private readonly IValidatorBuilder<TEntity> validatorBuilder;
		private readonly Expression<Func<TEntity, TProperty>> getValue;

		public RuleBuilder(IValidatorBuilder<TEntity> validatorBuilder, Expression<Func<TEntity, TProperty>> getValue)
		{
			this.validatorBuilder = validatorBuilder;
			this.getValue = getValue;
		}

		public IValidatorBuilderWithMessageBuilder<TEntity, TProperty> Custom(Expression<Func<TProperty, bool>> rule)
		{
			return new ValidatorBuilderWithMessageBuilder<TEntity, TProperty>(
				validatorBuilder,
				new MessageBuilder<TEntity, TProperty>(validatorBuilder, getValue, rule)
			);
		}
	}
}