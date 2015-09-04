using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class GeneralRulesExtensions
	{
		public static IExpressionBuilder<TEntity, TProperty> IsNotNull<TEntity, TProperty>(this IExpressionBuilder<TEntity, TProperty> ruleBuilder, string property)
		{
			var messageBuilder = ruleBuilder.Expression(t => t != null);
			messageBuilder.Message(string.Format("{0} cannot be Null", property));
			return messageBuilder;
		}
	}
}