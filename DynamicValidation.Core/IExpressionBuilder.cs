using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IExpressionBuilder<TEntity, TProperty> : IValidatorBuilder<TEntity>, INotBuilder<TEntity, TProperty>
	{
		/// <summary>
		/// Expressions the specified expression.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> Expression(Expression<Func<TProperty, bool>> expression);

		/// <summary>
		/// Validators the specified inner valdiator.
		/// </summary>
		/// <param name="innerValdiator">The inner valdiator.</param>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> Validator(IValidator<TProperty> innerValdiator);

		/// <summary>
		/// Validators the specified inner validator builder.
		/// </summary>
		/// <param name="innerValidatorBuilder">The inner validator builder.</param>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> Validator(Action<IValidatorBuilder<TProperty>> innerValidatorBuilder);

		/// <summary>
		/// Set rule Message for fail.
		/// </summary>
		/// <param name="message">Fail message</param>
		/// <remarks>
		/// In fail message, some template fields can be used:
		///  - %member% - Name of the validated member
		///  - %get_value% - Expression used to return member value
		///  - %name% - Name of the rule
		///  - %expression% - Validation expression
		///  - %when% - when expression which determines rule is appliable
		///  - %stop% - "True" if rule is stopping validation, otherwise "false"
		///  - %validator% - Name of inner validator, if rule uses it and it has name
		/// </remarks>
		IExpressionBuilder<TEntity, TProperty> Message(string message);

		/// <summary>
		/// Nameds the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> Named(string name);

		/// <summary>
		/// Whens the specified when.
		/// </summary>
		/// <param name="when">The when.</param>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> When(Expression<Func<TEntity, bool>> when);

		/// <summary>
		/// Allows setting of negative bool parameters of rule;
		/// </summary>
		/// <value>
		/// Negative values interface.
		/// </value>
		INotBuilder<TEntity, TProperty> Not { get; }
	}

	public interface INotBuilder<TEntity, TProperty>
	{
		/// <summary>
		/// Do not stop processing of validation when false.
		/// </summary>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> Stop();
	}
}