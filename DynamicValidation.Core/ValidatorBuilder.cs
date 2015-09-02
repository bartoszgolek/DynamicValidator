using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IValidatorBuilder<TEntity>
	{
		IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue);
	}

	internal class ValidatorBuilder<TEntity> : IValidatorBuilder<TEntity>
	{
		private readonly IList<RuleBuilder<TEntity>> builders = new List<RuleBuilder<TEntity>>();

		public IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue)
		{
			var messageBuilder = new MessageBuilder<TEntity>(this);
			var expressionBuilder = new ExpressionBuilder<TEntity, TProperty>(messageBuilder, this);
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