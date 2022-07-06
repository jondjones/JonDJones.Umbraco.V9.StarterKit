using System;

namespace JonDJones.Core.Interfaces.Example
{
    public class GetId : ISingleton, ITransient, IScoped
    {
        public GetId()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
    }
}
