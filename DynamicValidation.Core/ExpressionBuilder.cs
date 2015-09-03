using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ExpressionBuilder<TEntity, TProperty> : IExpressionBuilder<TEntity, TProperty>
	{
		private readonly MessageBuilder<TEntity> messageBuilder;
		private Expression<Func<TProperty, bool>> expression;

		public ExpressionBuilder(MessageBuilder<TEntity> messageBuilder)
		{
			this.messageBuilder = messageBuilder;
		}

		public IMessageBuilder<TEntity> WithExpression(Expression<Func<TProperty, bool>> expression)
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