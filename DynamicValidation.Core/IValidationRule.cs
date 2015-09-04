namespace DynamicValidation.Core
{
	internal interface IValidationRule<in TEntity>
	{
		RuleResult Validate(TEntity entity);

		bool Stop { get; }
	}
}