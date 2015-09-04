using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using DynamicValidation.Core;
using DynamicValidation.Extensions;

namespace ValidateShowCase
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<Interactor>();
			builder.Register(
				c => Validator.For<TestEntity>(validatorBuilder => validatorBuilder
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
				).As<IValidator<TestEntity>>();

			var container = builder.Build();

			var interactor = container.Resolve<Interactor>();
			interactor.Run();

			Console.ReadLine();
		}
	}

	internal class Interactor
	{
		private readonly IValidator<TestEntity> validator;

		public Interactor(IValidator<TestEntity> validator)
		{
			this.validator = validator;
		}

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
	}

	internal class TestEntity
	{
		public string StringProperty { get; set; }
		public int IntProperty { get; set; }
		public decimal DecimalProperty { get; set; }
		public TestSubEntity SubEntity { get; set; }
		public TestSubEntity SubEntity2 { get; set; }
	}

	internal class TestSubEntity
	{
		public string SubStringProperty { get; set; }
	}
}