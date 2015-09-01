using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ValidatorBuilderWithMessageBuilder<TEntity, TProperty> : IValidatorBuilderWithMessageBuilder<TEntity, TProperty>
	{
		private readonly IValidatorBuilder<TEntity> validatorBuilder;
		private readonly IMessageBuilder<TEntity> messageBuilder;

		public ValidatorBuilderWithMessageBuilder(IValidatorBuilder<TEntity> validatorBuilder, IMessageBuilder<TEntity> messageBuilder)
		{
			this.validatorBuilder = validatorBuilder;
			this.messageBuilder = messageBuilder;
		}

		public ValidatorBuilder<TEntity> WithRule<TProperty1>(
			Expression<Func<TEntity, TProperty1>> getValue,
			Expression<Func<TProperty1, bool>> rule, string message)
		{
			messageBuilder.WithMessage(string.Empty);
			return validatorBuilder.WithRule(getValue, rule, message);
		}

		public IValidator<TEntity> Create()
		{
			messageBuilder.WithMessage(string.Empty);
			return validatorBuilder.Create();
		}

		public IValidatorBuilder<TEntity> WithMessage(string message)
		{
			return messageBuilder.WithMessage(message);
		}
	}
}