namespace Fw.Infrastructure.Persistance.Common.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class NotAuditable : Attribute
{
    public bool Enabled { get; set; }

    public NotAuditable(bool nonAuditable = true)
    {
        this.Enabled = nonAuditable;
    }
}