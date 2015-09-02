using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ExpressionBuilder<TEntity, TProperty> : IExpressionBuilder<TEntity, TProperty>
	{
		private readonly MessageBuilder<TEntity> messageBuilder;
		private Expression<Func<TProperty, bool>> expression;
		private readonly ValidatorBuilder<TEntity> validatorBuilder;

		public ExpressionBuilder(MessageBuilder<TEntity> messageBuilder, ValidatorBuilder<TEntity> validatorBuilder)
		{
			this.messageBuilder = messageBuilder;
			this.validatorBuilder = validatorBuilder;
		}

		public IMessageBuilder<TEntity> Custom(Expression<Func<TProperty, bool>> expression)
		{
			this.expression = expression;
			return messageBuilder;
		}

		public IValidationRule<TEntity> CreateRule(Expression<Func<TEntity, TProperty>> getValueExpr)
		{
			return messageBuilder.CreateRule(getValueExpr, expression);
		}
	}
}