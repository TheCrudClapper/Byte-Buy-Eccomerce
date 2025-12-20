namespace ByteBuy.API.Attributes;

public class ResourceAttribute : Attribute
{
    public string Name { get; }

    public ResourceAttribute(string name)
    {
        Name = name;
    }
}
