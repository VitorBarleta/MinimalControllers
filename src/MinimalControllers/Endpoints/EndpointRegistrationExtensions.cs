using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using MinimalControllers.Attributes;

namespace MinimalControllers.Endpoints;

public static class EndpointRegistrationExtensions
{
    private static readonly Type _endpointType = typeof(IEndpoint);

    ///<summary>
    /// <param name="assembly">The assembly where the minimal controllers are in.</param>
    ///</summary>
    public static void UseMinimalControllers(this WebApplication applicationBuilder, Assembly assembly)
    {
        var builtEndpoints = assembly
            .ExportedTypes
            .Where(t => IsCompatibleEndpointType(t))
            .Select(t => BuildEndpoints(applicationBuilder, t));

        foreach (var builtEndpoint in builtEndpoints)
        {
            MinimalControllerAttribute? routeAttribute =
                (MinimalControllerAttribute?)Attribute.GetCustomAttribute(
                    builtEndpoint?.GetType()!,
                    typeof(MinimalControllerAttribute))
                ?? throw new Exception();

            if (builtEndpoint is not null)
                applicationBuilder.MapMethods(
                    routeAttribute.Path,
                    routeAttribute.Methods.AsEnumerable(),
                    CreateDelegate(builtEndpoint.GetType().GetMethod(IEndpoint.HandleDefinitionName)
                        ?? throw new ArgumentException(IEndpoint.HandleDefinitionName), builtEndpoint)!);
        }
    }

    private static object? BuildEndpoints(
        IApplicationBuilder appBuilder,
        Type type)
    {
        var constructor = type
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .SingleOrDefault() ?? throw new Exception($"The type \'{type.Name}\' has more than one constructor.");

        var constructorParams = constructor.GetParameters();

        if (constructorParams.Any())
            return constructor.Invoke(
                GetConstructorParamInstances(appBuilder.ApplicationServices, constructorParams));

        return constructor.Invoke(null);
    }

    private static object?[] GetConstructorParamInstances(
        IServiceProvider serviceProvider,
        ParameterInfo[] value) =>
        value.Select(v => serviceProvider.GetService(v.ParameterType)).ToArray();

    private static bool IsCompatibleEndpointType(Type type) =>
        type.IsClass
        && !type.IsAbstract
        && type.IsPublic
        && type.GetInterfaces()
               .Any(ti => ti == _endpointType);

    public static Delegate CreateDelegate(MethodInfo methodInfo, object target)
    {
        Func<Type[], Type> getType;
        var isAction = methodInfo.ReturnType.Equals((typeof(void)));
        var types = methodInfo.GetParameters().Select(p => p.ParameterType);

        if (isAction)
        {
            getType = Expression.GetActionType;
        }
        else
        {
            getType = Expression.GetFuncType;
            types = types.Concat(new[] { methodInfo.ReturnType });
        }

        if (methodInfo.IsStatic)
        {
            return Delegate.CreateDelegate(getType(types.ToArray()), methodInfo);
        }

        return Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo);
    }
}
