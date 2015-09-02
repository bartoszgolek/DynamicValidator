using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal abstract class RuleBuilder<TEntity>
	{
		public abstract IValidationRule<TEntity> CreateRule();
	}

	internal class RuleBuilder<TEntity, TProperty> : RuleBuilder<TEntity>
	{
		private readonly ExpressionBuilder<TEntity, TProperty> expressionBuilder;
		private readonly Expression<Func<TEntity, TProperty>> getValueExpr;

		public RuleBuilder(ExpressionBuilder<TEntity, TProperty> expressionBuilder, Expression<Func<TEntity, TProperty>> getValueExpr)
		{
			this.expressionBuilder = expressionBuilder;
			this.getValueExpr = getValueExpr;
		}

		public override IValidationRule<TEntity> CreateRule()
		{
			return expressionBuilder.CreateRule(getValueExpr);
		}
	}
}