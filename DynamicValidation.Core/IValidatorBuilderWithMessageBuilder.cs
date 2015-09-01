namespace DynamicValidation.Core
{
	public interface IValidatorBuilderWithMessageBuilder<TEntity, TProperty> : IValidatorBuilder<TEntity>, IMessageBuilder<TEntity>
	{
	}
}