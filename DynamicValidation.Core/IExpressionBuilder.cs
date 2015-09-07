using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IExpressionBuilder<TEntity, TProperty> : IValidatorBuilder<TEntity>, INotBuilder<TEntity, TProperty>
	{
		IExpressionBuilder<TEntity, TProperty> Expression(Expression<Func<TProperty, bool>> expression);

		IExpressionBuilder<TEntity, TProperty> Validator(IValidator<TProperty> innerValdiator);

		IExpressionBuilder<TEntity, TProperty> Validator(Action<IValidatorBuilder<TProperty>> innerValidatorBuilder);

		IExpressionBuilder<TEntity, TProperty> Message(string message);

		IExpressionBuilder<TEntity, TProperty> When(Expression<Func<TProperty, bool>> when);

		INotBuilder<TEntity, TProperty> Not { get; }
	}

	public interface INotBuilder<TEntity, TProperty>
	{
		IExpressionBuilder<TEntity, TProperty> Stop();
	}
}