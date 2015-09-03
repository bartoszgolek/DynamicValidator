using System;
using System.Collections.Generic;
using System.Linq;

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

		public Validator(IEnumerable<IValidationRule<T>> rules)
		{
			this.rules = new List<IValidationRule<T>>(rules);
		}

		public ValidationResult Validate(T entity)
		{
			return new ValidationResult(rules.Select(validationRule => validationRule.Validate(entity)));
		}
	}
}