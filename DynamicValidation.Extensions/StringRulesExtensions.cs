using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class StringRulesExtensions
	{
		public static IExpressionBuilder<TEntity, string> HasValue<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder)
		{
			return ruleBuilder.Expression(t => t != null).Message("%member% cannot be Null");
		}

		public static IExpressionBuilder<TEntity, string> MaxLength<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder, int length)
		{
			return ruleBuilder.Expression(s => s.Length < length).Message(string.Format("%member% cannot be longer than {0} chars", length));
		}

		public static IExpressionBuilder<TEntity, string> MinLength<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder, int length)
		{
			return ruleBuilder.Expression(s => s.Length > length).Message(string.Format("%member% cannot be shorter than {0} chars", length));
		}
	}
}