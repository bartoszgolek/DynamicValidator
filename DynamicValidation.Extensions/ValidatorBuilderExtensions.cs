using System;
using System.Linq.Expressions;
using DynamicValidation.Core;

namespace DynamicValidation.Extensions
{
	public static class ValidatorBuilderExtensions
	{
		public static IRuleBuilder<TEntity, TProperty> Assert<TEntity, TProperty>(
			this ValidatorBuilder<TEntity> validatorBuilder, Expression<Func<TEntity, TProperty>> getValue)
		{
			return new RuleBuilder<TEntity, TProperty>(validatorBuilder, getValue);
		}
	}
}