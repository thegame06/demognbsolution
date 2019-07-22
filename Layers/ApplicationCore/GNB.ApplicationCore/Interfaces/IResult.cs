namespace GNB.ApplicationCore.Interfaces
{
    public interface IResult
    {
        string StatusCode { get; set; }

        string StatusDescription { get; set; }

        bool IsSuccessStatusCode { get; }

        bool Successfully { get; }

        System.Exception Exception { get; }

        string GetString();
    }
}
