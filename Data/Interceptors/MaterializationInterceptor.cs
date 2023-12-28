using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Data.Interceptors;

internal class MaterializationInterceptor : IMaterializationInterceptor
{
    public MaterializationInterceptor()
    {
        
    }

    public object CreatingInstance(MaterializationInterceptionData materializationData, InterceptionResult<object> result)
    {
        return result;
    }

    public object CreatedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        return entity;
    }

    public object InitializingInstance(MaterializationInterceptionData materializationData, object entity, InterceptionResult result)
    {
        return result;
    }


    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        return entity;
    }
}
