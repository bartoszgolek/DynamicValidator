﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DynamicValidation.Core
{
	public interface IValidatorBuilder<TEntity>
	{
		ValidatorBuilder<TEntity> WithRule<TProperty>(
			Expression<Func<TEntity, TProperty>> getValue,
			Expression<Func<TProperty, bool>> rule,
			string message);

		IValidator<TEntity> Create();
	}

	public class ValidatorBuilder<TEntity> : IValidatorBuilder<TEntity>
	{
		private readonly IList<IValidationRule<TEntity>> rules = new List<IValidationRule<TEntity>>();

		public ValidatorBuilder<TEntity> WithRule<TProperty>(
			Expression<Func<TEntity, TProperty>> getValue,
			Expression<Func<TProperty, bool>> rule,
			string message)
		{
			rules.Add(new ValidationRule<TEntity, TProperty>(getValue, rule, message));

			return this;
		}

		public IValidator<TEntity> Create()
		{
			return new Validator<TEntity>(rules);
		}
	}
}