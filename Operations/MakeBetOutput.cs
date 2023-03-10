namespace Operations;

public class MakeBetOutput
{
    public IList<Error> Errors { get; set; }
    public IList<Selection> Selections { get; set; }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class Selection
    {
        public int Id { get; set; }
        public IList<Error> Errors { get; set; }
    }
}