using System;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IValidatorBuilder<TEntity>
	{
		/// <summary>
		/// Creates new rule on property returned from getValue expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="getValue">Expression to get value of property to validate.</param>
		/// <returns></returns>
		IExpressionBuilder<TEntity, TProperty> RuleOn<TProperty>(Expression<Func<TEntity, TProperty>> getValue);

		/// <summary>
		/// Sets name of the validator.
		/// </summary>
		/// <param name="name">Validator name</param>
		/// <returns></returns>
		IValidatorBuilder<TEntity> ValidatorName(string name);
	}
}