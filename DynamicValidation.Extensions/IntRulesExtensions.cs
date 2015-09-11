using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class IntRulesExtensions
	{
		public static IExpressionBuilder<TEntity, int?> HasValue<TEntity>(this IExpressionBuilder<TEntity, int?> ruleBuilder)
		{
			return ruleBuilder.Expression(t => t != null).Message("%member% cannot be Null");
		}

		public static IExpressionBuilder<TEntity, int> LessThan<TEntity>(this IExpressionBuilder<TEntity, int> ruleBuilder, int value)
		{
			return ruleBuilder.Expression(i => i < value).Message(string.Format("%member% has to be less than {0}", value));
		}

		public static IExpressionBuilder<TEntity, int> LessOrEqual<TEntity>(this IExpressionBuilder<TEntity, int> ruleBuilder, int value)
		{
			return ruleBuilder.Expression(i => i <= value).Message(string.Format("%member% has to be less or equal to {0}", value));
		}

		public static IExpressionBuilder<TEntity, int> GreaterThan<TEntity>(this IExpressionBuilder<TEntity, int> ruleBuilder, int value)
		{
			return ruleBuilder.Expression(i => i > value).Message(string.Format("%member% has to be greater than {0}", value));
		}

		public static IExpressionBuilder<TEntity, int> GreaterOrEqual<TEntity>(this IExpressionBuilder<TEntity, int> ruleBuilder, int value)
		{
			return ruleBuilder.Expression(i => i >= value).Message(string.Format("%member% has to be greater or equal to {0}", value));
		}

		public static IExpressionBuilder<TEntity, int> Equal<TEntity>(this IExpressionBuilder<TEntity, int> ruleBuilder, int value)
		{
			return ruleBuilder.Expression(i => i == value).Message(string.Format("%member% has to be equal to {0}", value));
		}
	}
}