namespace ByteBuy.Core.ResultTypes;

public static class CompanyInfoErrors
{
    public static readonly Error MultipleCompanyInfos = new Error(
        400, "There is already an company info in database, update it");
}
