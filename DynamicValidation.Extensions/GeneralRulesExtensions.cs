using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class GeneralRulesExtensions
	{
		public static IValidatorBuilder<TEntity> IsNotNull<TEntity, TProperty>(this IRuleBuilder<TEntity, TProperty> ruleBuilder, string property)
		{
			return ruleBuilder.Custom(t => t != null).WithMessage(string.Format("{0} cannot be Null", property));
		}
	}
}