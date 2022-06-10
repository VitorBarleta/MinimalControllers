namespace MinimalControllers.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public partial class MinimalControllerAttribute : Attribute
{
    public string Path { get; }
    public string[] Methods { get; }

    public MinimalControllerAttribute(string path, params string[] methods)
    {
        Path = path;
        Methods = methods;
    }
}
