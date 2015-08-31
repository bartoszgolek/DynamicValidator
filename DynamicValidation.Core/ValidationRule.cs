using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ValidationRule<TEntity, TProperty> : IValidationRule<TEntity>
	{
		private readonly Expression<Func<TEntity, TProperty>> getValueExpr;
		private readonly string message;
		private readonly Func<TProperty, bool> rule;
		private readonly Func<TEntity, TProperty> getValue;

		public ValidationRule(Expression<Func<TEntity, TProperty>> getValueExpr, Func<TProperty, bool> rule, string message)
		{
			this.getValueExpr = getValueExpr;
			getValue = getValueExpr.Compile();
			this.message = message;
			this.rule = rule;
		}

		public RuleResult Validate(TEntity entity)
		{
			bool result = rule(getValue(entity));

			var x = getValueExpr.Body as MemberExpression;

			return new RuleResult(result, x != null ? x.Member.Name : string.Empty, result ? string.Empty : message);
		}
	}
}