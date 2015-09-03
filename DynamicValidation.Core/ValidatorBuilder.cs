using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class ValidatorBuilder<TEntity> : IValidatorBuilder<TEntity>
	{
		private readonly IList<IRuleBuilder<TEntity>> builders = new List<IRuleBuilder<TEntity>>();

		public IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue)
		{
			var messageBuilder = new MessageBuilder<TEntity>(this);
			var expressionBuilder = new ExpressionBuilder<TEntity, TProperty>(messageBuilder);
			var ruleBuilder = new RuleBuilder<TEntity, TProperty>(expressionBuilder, getValue);
			builders.Add(ruleBuilder);
			return expressionBuilder;
		}

		public IValidator<TEntity> GetValidator()
		{
			var rules = builders.Select(rb => rb.CreateRule()).ToList();
			return new Validator<TEntity>(rules);
		}
	}
}