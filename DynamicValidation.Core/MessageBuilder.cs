using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class MessageBuilder<TEntity> : IMessageBuilder<TEntity>
	{
		private readonly ValidatorBuilder<TEntity> validatorBuilder;
		private string message = string.Empty;

		public MessageBuilder(ValidatorBuilder<TEntity> validatorBuilder)
		{
			this.validatorBuilder = validatorBuilder;
		}

		public IValidatorBuilder<TEntity> WithMessage(string message)
		{
			this.message = message;
			return validatorBuilder;
		}

		public IValidationRule<TEntity> CreateRule<TProperty>(
			Expression<Func<TEntity, TProperty>> getValueExpr,
			Expression<Func<TProperty, bool>> ruleExpr)
		{
			return new ValidationRule<TEntity, TProperty>(getValueExpr, ruleExpr, message);
		}

		public IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue)
		{
			return validatorBuilder.RuleOn(getValue);
		}
	}
}