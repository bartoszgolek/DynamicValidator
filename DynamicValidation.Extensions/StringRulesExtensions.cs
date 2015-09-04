using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class StringRulesExtensions
	{
		public static IExpressionBuilder<TEntity, string> HasValue<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder,
																string property)
		{
			var messageBuilder = ruleBuilder.Expression(s => !string.IsNullOrEmpty(s));
			messageBuilder.Message(string.Format("{0} cannot be Null or Empty", property));
			return messageBuilder;
		}

		public static IExpressionBuilder<TEntity, string> MaxLength<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder, int length,
																	string property)
		{
			var messageBuilder = ruleBuilder.Expression(s => s.Length < length);
			messageBuilder.Message(string.Format("{0} cannot be longer than {1} chars", property, length));
			return messageBuilder;
		}

		public static IExpressionBuilder<TEntity, string> MinLength<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder, int length,
																	string property)
		{
			var messageBuilder = ruleBuilder.Expression(s => s.Length > length);
			messageBuilder.Message(string.Format("{0} cannot be shorter than {1} chars", property, length));
			return messageBuilder;
		}
	}
}