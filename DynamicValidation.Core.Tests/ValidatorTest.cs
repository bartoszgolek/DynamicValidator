using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace DynamicValidation.Core.Tests
{
	[TestFixture]
	public class ValidatorTest
	{
		private readonly Expression<Func<string, bool>> unvalidRule = s => false;
		private readonly Expression<Func<string, bool>> validRule = s => true;

		[Test]
		public void ValidatorWithoutRulesWillReturnValidResult()
		{
			var validator = Validator.For<string>(builder => { });
			var result = validator.Validate(null);

			Assert.IsTrue(result.Result);
		}

		[Test]
		public void ValidatorWithFalseRuleWillReturnUnvalidResult()
		{
			var validator = Validator.For<string>(builder => builder.RuleOn(s => s).Expression(unvalidRule));
			var result = validator.Validate(null);

			Assert.IsFalse(result.Result);
		}

		[Test]
		public void ValidatorWithValidRuleWillReturnValidResult()
		{
			var validator = Validator.For<string>(builder => builder.RuleOn(s => s).Expression(validRule));
			var result = validator.Validate(null);

			Assert.IsTrue(result.Result);
		}

		[Test]
		public void ValidatorWithValidRuleWillReturnResultWithValidDetail()
		{
			var validator = Validator.For<string>(builder => builder.RuleOn(s => s).Expression(validRule));
			var result = validator.Validate(null);

			Assert.IsTrue(result.Details.First().Result);
		}

		[Test]
		public void ValidatorWithUnvalidRuleWillReturnResultWithUnvalidDetail()
		{
			var validator = Validator.For<string>(builder => builder.RuleOn(s => s).Expression(unvalidRule));
			var result = validator.Validate(null);

			Assert.IsFalse(result.Details.First().Result);
		}

		[Test]
		public void ValidatorWithValidRuleWillNotReturnMessageInRuleResult()
		{
			var validator = Validator.For<string>(builder => builder.RuleOn(s => s).Expression(validRule));
			var result = validator.Validate(null);

			Assert.AreEqual(String.Empty, result.Details.First().Message);
		}

		[Test]
		public void ValidatorWithValidRuleWithoutMessageWillReturnDefaultMessageInRuleResult()
		{
			var validator = Validator.For<string>(builder => builder.RuleOn(s => s).Expression(unvalidRule));
			var result = validator.Validate(null);

			Assert.AreEqual("s => False", result.Details.First().Message);
		}

		[Test]
		public void ValidatorWithValidRuleWithoutMessageWillReturnDefaultMessageWithPropertyNameWhenMatchedForPropertyInRuleResult()
		{
			var validator = Validator.For<SimpleTestEntity>(builder => builder.RuleOn(te => te.S).Expression(unvalidRule));
			var result = validator.Validate(new SimpleTestEntity());

			Assert.AreEqual("S: s => False", result.Details.First().Message);
		}

		[Test]
		public void ValidatorWithValidRuleWithoutMessageWillReturnCustomMessageWhenMessageIsSetInRuleResult()
		{
			var validator = Validator.For<SimpleTestEntity>(builder =>
				builder.RuleOn(te => te.S).Expression(unvalidRule).Message("custom")
			);
			var result = validator.Validate(new SimpleTestEntity());

			Assert.AreEqual("custom", result.Details.First().Message);
		}

		[Test]
		public void ValidatorWillSkipAndReturnValidResultForRuleWithFalseWhen()
		{
			var validator = Validator.For<SimpleTestEntity>(builder =>
				builder.RuleOn(te => te.S).Expression(unvalidRule).Message("custom").When(s => false)
			);
			var result = validator.Validate(new SimpleTestEntity());

			Assert.IsTrue(result.Details.First().Result);
		}

		private class SimpleTestEntity
		{
			public string S { get; set; }
		}
	}
}