namespace Operations.Operations.Shared;

public class ValidationResponse
{
	public IList<Error> Errors { get; } = new List<Error>();
	public IList<Selection> Selections { get; } = new List<Selection>();

	public class Error
	{
		public int Code { get; set; }
		public string Message { get; set; }
	}

	public class Selection
	{
		public int Id { get; set; }
		public IList<Error> Errors { get; set; } = new List<Error>();
	}
}