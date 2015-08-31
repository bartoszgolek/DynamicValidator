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
				c =>
					new ValidatorBuilder<TestEntity>()
						.WithRule(t => t.StringProperty, s => !string.IsNullOrEmpty(s), "StringProperty cannot be empty")
						.WithRule(t => t.IntProperty, i => i > 0, "IntProperty have to be greater than zero")
						.Assert(t => t.IntProperty).Custom(i => i < 10, "IntProperty have to be less than ten")
						.Assert(t => t.StringProperty).HasValue("StringProperty")
						.Assert(t => t.StringProperty).MaxLength(10, "StringProperty")
						.Assert(t => t).Custom(t => t.IntProperty == 5 && t.StringProperty != null, "CUstom message")
						.Assert(t => t.DecimalProperty).IsNotNull("DecimalProperty")
						.Assert(t => t.IntProperty).IsNotNull("IntProperty")
						.Create()
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