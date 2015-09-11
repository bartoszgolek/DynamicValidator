using System;
using System.Collections.Generic;

namespace DynamicValidation.Core
{
	public static class Validator
	{
		public static IValidator<TEntity> For<TEntity>(Action<IValidatorBuilder<TEntity>> build)
		{
			var validatorBuilder = new ValidatorBuilder<TEntity>();
			build(validatorBuilder);
			return validatorBuilder.GetValidator();
		}
	}

	internal class Validator<T> : IValidator<T>
	{
		private readonly IList<IValidationRule<T>> rules;
		private readonly string name;

		public Validator(IEnumerable<IValidationRule<T>> rules, string name)
		{
			this.name = name;
			this.rules = new List<IValidationRule<T>>(rules);
		}

		public ValidationResult Validate(T entity)
		{
			IList<RuleResult> ruleResults = new List<RuleResult>();
			foreach (var rule in rules)
			{
				var ruleResult = rule.Validate(entity);
				ruleResults.Add(ruleResult);
				if (rule.Stop)
					break;
			}

			return new ValidationResult(ruleResults);
		}

		public string Name
		{
			get { return name; }
		}
	}
}