using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class IntRulesExtensions
	{
		public static IMessageBuilder<TEntity> HasValue<TEntity>(this IExpressionBuilder<TEntity, int?> ruleBuilder, string property)
		{
			var messageBuilder = ruleBuilder.Custom(i => !i.HasValue);
			messageBuilder.WithMessage(string.Format("{0} cannot be Null", property));
			return messageBuilder;
		}
	}
}