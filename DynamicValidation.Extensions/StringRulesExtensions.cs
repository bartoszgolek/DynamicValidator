using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class StringRulesExtensions
	{
		public static IValidatorBuilder<TEntity> HasValue<TEntity>(this IRuleBuilder<TEntity, string> ruleBuilder,
																string property)
		{
			return ruleBuilder.Custom(s => !string.IsNullOrEmpty(s)).WithMessage(string.Format("{0} cannot be Null or Empty", property));
		}

		public static IValidatorBuilder<TEntity> MaxLength<TEntity>(this IRuleBuilder<TEntity, string> ruleBuilder, int length,
																	string property)
		{
			return ruleBuilder.Custom(s => s.Length < length).WithMessage(string.Format("{0} cannot be longer than {1}", property, length));
		}
	}
}