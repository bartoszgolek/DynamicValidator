using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ValidationRule<TEntity, TProperty> : IValidationRule<TEntity>
	{
		private readonly Expression<Func<TEntity, TProperty>> getValueExpr;
		private readonly Expression<Func<TProperty, bool>> ruleExpr;
		private readonly string message;
		private readonly bool stop;

		private readonly Func<TEntity, TProperty> getValue;
		private readonly Func<TEntity, bool> when;
		private readonly Func<TProperty, bool> rule;
		private readonly IValidator<TProperty> innerValdiator;

		public ValidationRule(
			Expression<Func<TEntity, TProperty>> getValueExpr,
			Expression<Func<TEntity, bool>> whenExpr,
			Expression<Func<TProperty, bool>> ruleExpr,
			string message,
			bool stop,
			IValidator<TProperty> innerValdiator)
		{
			this.getValueExpr = getValueExpr;
			this.ruleExpr = ruleExpr;
			this.message = message;
			this.stop = stop;
			this.innerValdiator = innerValdiator;

			getValue = getValueExpr.Compile();
			when = (whenExpr ?? (property => true)).Compile();
			rule = ruleExpr != null ? ruleExpr.Compile() : null;
		}

		public RuleResult Validate(TEntity entity)
		{
			if (!when(entity))
				return RuleResult.Valid(GetMemberName());

			var propertyValue = getValue(entity);

			bool result = true;
			if (rule != null)
				result &= rule(propertyValue);

			ValidationResult innerResult = null;
			if (innerValdiator != null)
			{
				innerResult = innerValdiator.Validate(propertyValue);
				result &= innerResult.Result;
			}

			return new RuleResult(result, GetMemberName(), result ? string.Empty : GetMessage(), innerResult);
		}

		public bool Stop
		{
			get { return stop; }
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
			string defaultMessage = ruleExpr != null ? ruleExpr.ToString() : string.Empty;
			var memberName = GetMemberName();
			if (!string.IsNullOrEmpty(memberName))
				defaultMessage = memberName + ": " + defaultMessage;

			return defaultMessage;
		}
	}
}