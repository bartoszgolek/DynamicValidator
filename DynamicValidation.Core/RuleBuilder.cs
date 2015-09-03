using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class RuleBuilder<TEntity, TProperty> : IRuleBuilder<TEntity>
	{
		private readonly ExpressionBuilder<TEntity, TProperty> expressionBuilder;
		private readonly Expression<Func<TEntity, TProperty>> getValueExpr;

		public RuleBuilder(ExpressionBuilder<TEntity, TProperty> expressionBuilder, Expression<Func<TEntity, TProperty>> getValueExpr)
		{
			this.expressionBuilder = expressionBuilder;
			this.getValueExpr = getValueExpr;
		}

		public IValidationRule<TEntity> CreateRule()
		{
			return expressionBuilder.CreateRule(getValueExpr);
		}
	}
}