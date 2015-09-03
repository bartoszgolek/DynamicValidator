using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class GeneralRulesExtensions
	{
		public static IMessageBuilder<TEntity> IsNotNull<TEntity, TProperty>(this IExpressionBuilder<TEntity, TProperty> ruleBuilder, string property)
		{
			var messageBuilder = ruleBuilder.WithExpression(t => t != null);
			messageBuilder.WithMessage(string.Format("{0} cannot be Null", property));
			return messageBuilder;
		}
	}
}