using System.Collections.Generic;
using System.Linq;

namespace DynamicValidation.Core
{
	public class ValidationResult
	{
		public ValidationResult(IEnumerable<RuleResult> details)
		{
			this.details = new List<RuleResult>(details);
		}

		private readonly IEnumerable<RuleResult> details;

		public bool Result
		{
			get
			{
				return details.All(d => d.Result);
			}
		}

		public IEnumerable<RuleResult> Details
		{
			get
			{
				return details;
			}
		}
	}
}