using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public class MessageBuilder<TEntity, TProperty> : IMessageBuilder<TEntity>
	{
		private readonly IValidatorBuilder<TEntity> validatorBuilder;
		private readonly Expression<Func<TEntity, TProperty>> getValue;
		private readonly Expression<Func<TProperty, bool>> rule;

		public MessageBuilder(IValidatorBuilder<TEntity> validatorBuilder, Expression<Func<TEntity, TProperty>> getValue, Expression<Func<TProperty, bool>> rule)
		{
			this.validatorBuilder = validatorBuilder;
			this.getValue = getValue;
			this.rule = rule;
		}

		public IValidatorBuilder<TEntity> WithMessage(string message)
		{
			return validatorBuilder.WithRule(getValue, rule, message);
		}
	}
}