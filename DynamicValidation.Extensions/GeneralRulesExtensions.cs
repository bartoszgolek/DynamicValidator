using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class GeneralRulesExtensions
	{
		public static IExpressionBuilder<TEntity, TProperty> IsNotNull<TEntity, TProperty>(this IExpressionBuilder<TEntity, TProperty> ruleBuilder)
		{
			return ruleBuilder.Expression(t => t != null).Message("%member% cannot be Null");
		}
	}
}