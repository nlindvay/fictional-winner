namespace Fw.Domain.Common.Enums;

public enum OrderStatus
{
    None,
    Draft,
    Submitted,
    Booked,
    Shipped,
    Delivered,
    Invoiced,
    Cancelled
}