using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class StringRulesExtensions
	{
		public static IMessageBuilder<TEntity> HasValue<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder,
																string property)
		{
			var messageBuilder = ruleBuilder.WithExpression(s => !string.IsNullOrEmpty(s));
			messageBuilder.WithMessage(string.Format("{0} cannot be Null or Empty", property));
			return messageBuilder;
		}

		public static IMessageBuilder<TEntity> MaxLength<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder, int length,
																	string property)
		{
			var messageBuilder = ruleBuilder.WithExpression(s => s.Length < length);
			messageBuilder.WithMessage(string.Format("{0} cannot be longer than {1} chars", property, length));
			return messageBuilder;
		}

		public static IMessageBuilder<TEntity> MinLength<TEntity>(this IExpressionBuilder<TEntity, string> ruleBuilder, int length,
																	string property)
		{
			var messageBuilder = ruleBuilder.WithExpression(s => s.Length > length);
			messageBuilder.WithMessage(string.Format("{0} cannot be shorter than {1} chars", property, length));
			return messageBuilder;
		}
	}
}