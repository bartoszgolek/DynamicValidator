namespace Validate
{
	public class RuleResult
	{
		public RuleResult(bool result, string message)
		{
			Result = result;
			Message = message;
		}

		public string Message { get; private set; }

		public bool Result { get; private set; }
	}
}