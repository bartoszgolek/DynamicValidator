using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	internal class RuleBuilder<TEntity, TProperty> : IRuleBuilder<TEntity>, IExpressionBuilder<TEntity, TProperty>
	{
		private readonly ValidatorBuilder<TEntity> validatorBuilder;
		private readonly Expression<Func<TEntity, TProperty>> getValueExpr;
		private Expression<Func<TProperty, bool>> expression;
		private Expression<Func<TEntity, bool>> when;
		private string message;
		private bool stop;
		private Func<IValidator<TProperty>> getInnerValdiator = () => null;

		public RuleBuilder(ValidatorBuilder<TEntity> validatorBuilder, Expression<Func<TEntity, TProperty>> getValueExpr)
		{
			this.validatorBuilder = validatorBuilder;
			this.getValueExpr = getValueExpr;
		}

		public IValidationRule<TEntity> CreateRule()
		{
			return new ValidationRule<TEntity, TProperty>(getValueExpr, when, expression, message, stop, getInnerValdiator());
		}

		public IExpressionBuilder<TEntity, TProperty1> RuleOn<TProperty1>(Expression<Func<TEntity, TProperty1>> getValue)
		{
			return validatorBuilder.RuleOn(getValue);
		}

		public IExpressionBuilder<TEntity, TProperty> Validator(IValidator<TProperty> innerValdiator)
		{
			getInnerValdiator = () => innerValdiator;
			return this;
		}

		public IExpressionBuilder<TEntity, TProperty> Validator(Action<IValidatorBuilder<TProperty>> innerValidatorBuilder)
		{
			getInnerValdiator = () => Core.Validator.For(innerValidatorBuilder);
			return this;
		}

		public IExpressionBuilder<TEntity, TProperty> Message(string message)
		{
			this.message = message;
			return this;
		}

		public IExpressionBuilder<TEntity, TProperty> When(Expression<Func<TEntity, bool>> when)
		{
			this.when = when;
			return this;
		}

		public INotBuilder<TEntity, TProperty> Not
		{
			get { return new NotBuilder(this); }
		}

		public IExpressionBuilder<TEntity, TProperty> Expression(Expression<Func<TProperty, bool>> expression)
		{
			this.expression = expression;
			return this;
		}

		public IExpressionBuilder<TEntity, TProperty> Stop()
		{
			stop = true;
			return this;
		}

		internal class NotBuilder : INotBuilder<TEntity, TProperty>
		{
			private readonly RuleBuilder<TEntity, TProperty> ruleBuilder;

			public NotBuilder(RuleBuilder<TEntity, TProperty> ruleBuilder)
			{
				this.ruleBuilder = ruleBuilder;
			}

			public IExpressionBuilder<TEntity, TProperty> Stop()
			{
				ruleBuilder.stop = false;
				return ruleBuilder;
			}
		}
	}
}