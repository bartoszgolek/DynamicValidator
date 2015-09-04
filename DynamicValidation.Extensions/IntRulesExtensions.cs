using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class IntRulesExtensions
	{
		public static IExpressionBuilder<TEntity, int?> HasValue<TEntity>(this IExpressionBuilder<TEntity, int?> ruleBuilder, string property)
		{
			var messageBuilder = ruleBuilder.Expression(i => !i.HasValue);
			messageBuilder.Message(string.Format("{0} cannot be Null", property));
			return messageBuilder;
		}
	}
}