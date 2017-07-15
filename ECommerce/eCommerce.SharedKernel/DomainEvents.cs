using System;
using System.Collections.Generic;
using eCommerce.SharedKernel.Interfaces;
using StructureMap;

namespace eCommerce.SharedKernel
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> _actions;

        public static IContainer Container { get; set; }

        static DomainEvents()
        {
            Container = new Container();
        }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (_actions == null)
            {
                _actions = new List<Delegate>();
            }
            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handle in Container.GetAllInstances<IHandle<T>>())
            {
                handle.Handle(args);
            }

            if (_actions != null)
            {
                foreach (var action in _actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>) action)(args);
                    }
                }
            }
        }
    }
}