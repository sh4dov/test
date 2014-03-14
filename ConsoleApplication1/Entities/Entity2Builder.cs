using Entities;

namespace Factories
{
    public partial class Entity2Factory
    {
        partial void AfterBuild(Entity2 value, IValueProvider valueProvider)
        {
            value.Unknown = "After build value";
        }
    }
}