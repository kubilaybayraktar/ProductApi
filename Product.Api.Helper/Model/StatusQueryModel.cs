namespace Product.Api
{
    public class StatusQueryModel
    {
        [ValidateStatus]
        public short? Status { get; set; }
    }
}
