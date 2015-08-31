namespace Validate
{
	internal interface IValidationRule<in TEntity>
	{
		RuleResult Validate(TEntity entity);
	}
}