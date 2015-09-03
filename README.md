# DynamicValidator

Valdiation Library with fluent API.

I have started this to learn how to create fluent interfaces. Still I think it's quite usefull even in if it's simple.

Solution has 3 projects:
* __DynamicValidation.Core__ - main library,
* __DynamicValidation.Extensions__ - samples of extensions with some usful rules,
* __ValidateShowCase__ - console application with test usage and Autofac integration.

## Usage example

Create Validator:
```
var validator = Validator.For<TestEntity>(
  validatorBuilder => validatorBuilder
		.RuleOn(t => t.StringProperty).WithExpression(s => !string.IsNullOrEmpty(s)).WithMessage("StringProperty cannot be empty")
		.RuleOn(t => t.IntProperty).WithExpression(i => i > 0).WithMessage("IntProperty have to be greater than zero")
		.RuleOn(t => t.IntProperty).WithExpression(i => i < 10).WithMessage("IntProperty have to be less than ten")
		.RuleOn(t => t.IntProperty).WithExpression(i => i < 0)
		.RuleOn(t => t.StringProperty).HasValue("StringProperty")
		.RuleOn(t => t.StringProperty).MaxLength(10, "StringProperty").WithMessage("Max message")
		.RuleOn(t => t.StringProperty).MaxLength(5, "StringProperty")
		.RuleOn(t => t.StringProperty).MinLength(10, "StringProperty").WithMessage("Min message")
		.RuleOn(t => t).WithExpression(t => t.IntProperty == 5 && t.StringProperty != null).WithMessage("Custom message")
		.RuleOn(t => t.DecimalProperty).IsNotNull("DecimalProperty")
		.RuleOn(t => t.IntProperty).IsNotNull("IntProperty")
)
```

Use validator:
```
var t = new TestEntity
	{
		StringProperty = "111111111111111111111111111"
	};
var result = validator.Validate(t);

Console.WriteLine(
	result.Result
		? "Validation OK"
		: string.Format("Validation Failed: {0}\t{1}", Environment.NewLine,
			string.Join("," + Environment.NewLine + "\t", result.Details.Where(d => !d.Result).Select(d => d.Message))));
```

Create own rules with custom message:
```
	public static class GeneralRulesExtensions
	{
		public static IMessageBuilder<TEntity> IsNotNull<TEntity, TProperty>(this IExpressionBuilder<TEntity, TProperty> ruleBuilder, string property)
		{
			var messageBuilder = ruleBuilder.WithExpression(t => t != null);
			messageBuilder.WithMessage(string.Format("{0} cannot be Null", property));
			return messageBuilder;
		}
	}
```

Enjoy!
