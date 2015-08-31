using System;

namespace DynamicValidation.Core
{
	internal class ValidationRule<TEntity, TProperty> : IValidationRule<TEntity>
	{
		private readonly Func<TEntity, TProperty> getValue;
		private readonly string message;
		private readonly Func<TProperty, bool> rule;

		public ValidationRule(Func<TEntity, TProperty> getValue, Func<TProperty, bool> rule, string message)
		{
			this.getValue = getValue;
			this.message = message;
			this.rule = rule;
		}

		public RuleResult Validate(TEntity entity)
		{
			bool result = rule(getValue(entity));
			return new RuleResult(result, result ? string.Empty : message);
		}
	}
}