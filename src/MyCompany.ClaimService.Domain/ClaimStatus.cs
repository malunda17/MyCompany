namespace MyCompany.ClaimService.Domain
{
    public enum ClaimStatus : short
    {
        New = 1,
        Review,
        Pending,
        Approved,
        Closed
    }
}