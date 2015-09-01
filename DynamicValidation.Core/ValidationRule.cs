using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ValidationRule<TEntity, TProperty> : IValidationRule<TEntity>
	{
		private readonly Expression<Func<TEntity, TProperty>> getValueExpr;
		private readonly string message;
		private readonly Expression<Func<TProperty, bool>> ruleExpr;
		private readonly Func<TEntity, TProperty> getValue;
		private readonly Func<TProperty, bool> rule;

		public ValidationRule(Expression<Func<TEntity, TProperty>> getValueExpr, Expression<Func<TProperty, bool>> ruleExpr, string message)
		{
			this.getValueExpr = getValueExpr;
			getValue = getValueExpr.Compile();
			this.message = message;
			this.ruleExpr = ruleExpr;
			rule = ruleExpr.Compile();
		}

		public RuleResult Validate(TEntity entity)
		{
			bool result = rule(getValue(entity));

			return new RuleResult(result, GetMemberName(), result ? string.Empty : GetMessage());
		}

		private string GetMemberName()
		{
			var memberExpression = getValueExpr.Body as MemberExpression;
			return memberExpression != null ? memberExpression.Member.Name : string.Empty;
		}

		private string GetMessage()
		{
			return string.IsNullOrWhiteSpace(message) ? GetDefaultMessage() : message;
		}

		private string GetDefaultMessage()
		{
			string defaultMessage = ruleExpr.ToString();
			var memberName = GetMemberName();
			if (!string.IsNullOrEmpty(memberName))
				defaultMessage = memberName + ": " + ruleExpr;

			return defaultMessage;
		}
	}
}