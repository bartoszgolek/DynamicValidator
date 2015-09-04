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
    .RuleOn(t => t.StringProperty).Expression(s => !string.IsNullOrEmpty(s)).Message("StringProperty cannot be empty")
    .RuleOn(t => t.IntProperty).Expression(i => i > 0).Message("IntProperty have to be greater than zero")
    .RuleOn(t => t.IntProperty).Expression(i => i < 10).Message("IntProperty have to be less than ten")
    .RuleOn(t => t.IntProperty).Expression(i => i < 0)
    .RuleOn(t => t.StringProperty).HasValue("StringProperty")
    .RuleOn(t => t.StringProperty).MaxLength(10, "StringProperty").Message("Max message")
    .RuleOn(t => t.StringProperty).MaxLength(5, "StringProperty")
    .RuleOn(t => t.StringProperty).MinLength(10, "StringProperty").Message("Min message")
    .RuleOn(t => t).Expression(t => t.IntProperty == 5 && t.StringProperty != null).Message("Custom message")
    .RuleOn(t => t.DecimalProperty).IsNotNull("DecimalProperty")
    .RuleOn(t => t.IntProperty).When(i => false).Expression(i => false).Message("when false") //will not be used
    .RuleOn(t => t.IntProperty).When(i => true).Expression(i => false).Not.Stop().Message("when true") //will be used
    .RuleOn(t => t.IntProperty).Expression(i => false).Not.Stop().Message("not stop")
    .RuleOn(t => t.IntProperty).IsNotNull("IntProperty")
    .RuleOn(t => t.SubEntity).Expression(se => se != null).Message("SubEntity cannot be null")
    .RuleOn(t => t.SubEntity2).Expression(se => se != null).Message("SubEntity2 cannot be null")
    .RuleOn(t => t.SubEntity).When(se => se != null).Validator(Validator.For<TestSubEntity>(validatorBuilder1 => validatorBuilder1
      .RuleOn(ts => ts.SubStringProperty).Expression(s => !string.IsNullOrEmpty(s)).Message("SubEntity error")
    ))
    .RuleOn(t => t.IntProperty).Expression(i => false).Stop().Message("stop") //breaks validation
    .RuleOn(t => t.IntProperty).Expression(i => false).Message("After stop") //will not be used
)
```

Use validator:
```
public void Run()
{
  var t = new TestEntity
  {
    StringProperty = "111111111111111111111111111",
    SubEntity = new TestSubEntity()
  };
  var result = validator.Validate(t);

  Console.WriteLine(
    result.Result
      ? "Validation OK"
      : string.Format("Validation Failed: {0}", ReadDetails(result)));
}

private static string ReadDetails(ValidationResult result, int level = 1)
{
  var separator = "," + Environment.NewLine + new String('\t', level);
  IEnumerable<string> messages = result.Details.Where(d => !d.Result).Select(d => GetRuleResultMessage(d, level));

  return Environment.NewLine + new String('\t', level) + string.Join(separator, messages);
}

private static string GetRuleResultMessage(RuleResult d, int level)
{
  var subMessage = string.Empty;

  if (d.InnerResult != null)
    subMessage = ReadDetails(d.InnerResult, level + 1);

  return d.Message + subMessage;
}
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
