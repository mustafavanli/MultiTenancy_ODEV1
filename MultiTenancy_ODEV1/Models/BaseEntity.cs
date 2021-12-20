namespace MultiTenancy_ODEV1.Models
{
    public abstract class BaseEntity
    {
        public int TenantId { get; set; }
    }
}
