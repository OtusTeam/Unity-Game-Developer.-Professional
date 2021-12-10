using System;

namespace GameElements
{
    internal sealed class ServiceContext
    {
        private readonly GenericDictionary serviceMap;

        internal ServiceContext()
        {
            this.serviceMap = new GenericDictionary();
        }

        internal void AddService(object service)
        {
            this.AddRecursively(service);
        }

        internal void RemoveService(object service)
        {
            this.RemoveRecursively(service);
        }

        internal T GetService<T>()
        {
            return this.serviceMap.Get<T>();
        }

        internal bool TryGetService<T>(out T service)
        {
            return this.serviceMap.TryGet(out service);
        }

        private void AddRecursively(object service)
        {
            if (service is IGameServiceGroup group)
            {
                var services = group.GetServices();
                foreach (var innerService in services)
                {
                    this.AddRecursively(innerService);
                }
            }
            else if (!this.serviceMap.Add(service))
            {
                throw new Exception($"Service of type {service.GetType().Name} is already added!");
            }
        }

        private void RemoveRecursively(object service)
        {
            if (service is IGameServiceGroup group)
            {
                var services = group.GetServices();
                foreach (var innerService in services)
                {
                    this.RemoveRecursively(innerService);
                }
            }
            else
            {
                this.serviceMap.Remove(service);
            }
        }
    }
}