using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ValidatorBuilder<TEntity> : IValidatorBuilder<TEntity>
	{
		private readonly IList<IRuleBuilder<TEntity>> builders = new List<IRuleBuilder<TEntity>>();
		private string validatorName;

		public IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue)
		{
			var ruleBuilder = new RuleBuilder<TEntity, TProperty>(this, getValue);
			builders.Add(ruleBuilder);
			return ruleBuilder;
		}

		public IValidator<TEntity> GetValidator()
		{
			var rules = builders.Select(rb => rb.CreateRule()).ToList();
			return new Validator<TEntity>(rules, validatorName);
		}

		public IValidatorBuilder<TEntity> ValidatorName(string name)
		{
			validatorName = name;
			return this;
		}
	}
}