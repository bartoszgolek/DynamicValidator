using System.Collections.Generic;
using System.Linq;

namespace DynamicValidation.Core
{
	internal class Validator<T> : IValidator<T>
	{
		private readonly IList<IValidationRule<T>> rules;

		public Validator(IList<IValidationRule<T>> rules)
		{
			this.rules = rules;
		}

		public ValidationResult Validate(T entity)
		{
			return new ValidationResult(rules.Select(validationRule => validationRule.Validate(entity)));
		}
	}
}