using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class IntRulesExtensions
	{
		public static ValidatorBuilder<TEntity> HasValue<TEntity>(this IRuleBuilder<TEntity, int?> ruleBuilder, string property)
		{
			return ruleBuilder.Custom(i => !i.HasValue, string.Format("{0} cannot be Null", property));
		}
	}
}