using System;

namespace Validate
{
	public static class ValidatorBuilderExtensions
	{
		public static IRuleBuilder<TEntity, TProperty> Assert<TEntity, TProperty>(this ValidatorBuilder<TEntity> validatorBuilder, Func<TEntity, TProperty> getValue)
		{
			return new RuleBuilder<TEntity, TProperty>(validatorBuilder, getValue);
		}

		public static ValidatorBuilder<TEntity> HasValue<TEntity>(this IRuleBuilder<TEntity, string> ruleBuilder, string property)
		{
			return ruleBuilder.Custom(s => !string.IsNullOrEmpty(s), string.Format("{0} cannot be Null or Empty", property));
		}

		public static ValidatorBuilder<TEntity> MaxLength<TEntity>(this IRuleBuilder<TEntity, string> ruleBuilder, int length, string property)
		{
			return ruleBuilder.Custom(s => s.Length < length, string.Format("{0} cannot be longer than {1}", property, length));
		}

		public static ValidatorBuilder<TEntity> HasValue<TEntity>(this IRuleBuilder<TEntity, int?> ruleBuilder, string property)
		{
			return ruleBuilder.Custom(i => !i.HasValue, string.Format("{0} cannot be Null", property));
		}

		public static ValidatorBuilder<TEntity> IsNotNull<TEntity, TProperty>(this IRuleBuilder<TEntity, TProperty> ruleBuilder, string property)
		{
			return ruleBuilder.Custom(t => t != null, string.Format("{0} cannot be Null", property));
		}
	}
}