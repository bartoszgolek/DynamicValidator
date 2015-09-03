namespace DynamicValidation.Core
{
	internal interface IRuleBuilder<in TEntity>
	{
		IValidationRule<TEntity> CreateRule();
	}
}