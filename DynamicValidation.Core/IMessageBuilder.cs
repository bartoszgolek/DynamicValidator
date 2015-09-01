namespace DynamicValidation.Core
{
	public interface IMessageBuilder<TEntity>
	{
		IValidatorBuilder<TEntity> WithMessage(string message);
	}
}