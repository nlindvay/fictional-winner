using Fw.Domain.Common.Attributes;

namespace Fw.Infrastructure.Persistance.Common.Extensions;

public static class AuditUtilities
{
    public static bool IsNotAuditable(Type entity)
    {
        var attributes = entity.GetCustomAttributes(false);

        foreach (var customAttribute in attributes)
        {
            if (customAttribute.GetType() == typeof(NotAuditable))
            {
                var auditableAttribute = (NotAuditable)customAttribute;
                return auditableAttribute.Enabled;
            }
        }

        return false;
    }

    public static bool IsNotAuditable(Type entity, string propertyName)
    {
        if (propertyName == "Discriminator") //set Discriminator shadow property as non auditable
            return false;

        var attributes = entity.GetCustomAttributes(false);

        foreach (var customAttribute in attributes)
        {
            if (customAttribute.GetType() == typeof(NotAuditable))
            {
                var auditableAttribute = (NotAuditable)customAttribute;
                return auditableAttribute.Enabled;
            }
        }

        return false;
    }
}