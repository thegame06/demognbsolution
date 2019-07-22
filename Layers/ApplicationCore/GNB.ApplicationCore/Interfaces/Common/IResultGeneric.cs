namespace GNB.ApplicationCore.Interfaces
{
    public interface IResult<T> : IResult where T : class
    {
        T Resource { get; }
    }
}
