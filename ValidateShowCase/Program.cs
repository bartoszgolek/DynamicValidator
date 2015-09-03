using System;
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
					StringProperty = "111111111111111111111111111"
				};
			var result = validator.Validate(t);

			Console.WriteLine(
				result.Result
					? "Validation OK"
					: string.Format("Validation Failed: {0}\t{1}", Environment.NewLine,
						string.Join("," + Environment.NewLine + "\t", result.Details.Where(d => !d.Result).Select(d => d.Message))));
		}
	}

	internal class TestEntity
	{
		public string StringProperty { get; set; }
		public int IntProperty { get; set; }
		public decimal DecimalProperty { get; set; }
	}
}