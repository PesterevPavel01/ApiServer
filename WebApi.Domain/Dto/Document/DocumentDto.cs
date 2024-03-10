namespace WebApi.Domain.Dto.Document
{
    //public record DocumentDto(long Id,string Name, double Value,
    //    string Comment, string Expenditure, string Organization, string Department);
    public record DocumentDto(long Id, double Value,
    string Comment, short Expenditure, short Organization, short Department);
}
