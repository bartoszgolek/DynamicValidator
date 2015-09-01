using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class IntRulesExtensions
	{
		public static IValidatorBuilder<TEntity> HasValue<TEntity>(this IRuleBuilder<TEntity, int?> ruleBuilder, string property)
		{
			return ruleBuilder.Custom(i => !i.HasValue).WithMessage(string.Format("{0} cannot be Null", property));
		}
	}
}