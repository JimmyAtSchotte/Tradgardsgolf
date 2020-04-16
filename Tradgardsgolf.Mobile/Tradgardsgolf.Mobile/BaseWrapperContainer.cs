using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Resolving;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile
{
    public abstract class BaseWrapperContainer : IContainer
    {
        private readonly IContainer _container;

        public IDisposer Disposer => _container.Disposer;
        public object Tag => _container.Tag;
        public IComponentRegistry ComponentRegistry => _container.ComponentRegistry;

        public event EventHandler<LifetimeScopeBeginningEventArgs> ChildLifetimeScopeBeginning;
        public event EventHandler<LifetimeScopeEndingEventArgs> CurrentScopeEnding;
        public event EventHandler<ResolveOperationBeginningEventArgs> ResolveOperationBeginning;

        protected BaseWrapperContainer(IContainer container)
        {
            _container = container;
        }


        public T GetPage<T>()
        {
            return _container.Resolve<T>();
            //var pages = _container.Resolve<Page[]>();
            //return pages.FirstOrDefault(x => x.GetType() == typeof(T));
        }


        public ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }

        public ILifetimeScope BeginLifetimeScope(object tag)
        {
            return _container.BeginLifetimeScope(tag);
        }

        public ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return _container.BeginLifetimeScope(configurationAction);
        }

        public ILifetimeScope BeginLifetimeScope(object tag, Action<ContainerBuilder> configurationAction)
        {
            return _container.BeginLifetimeScope(tag, configurationAction);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _container.DisposeAsync();
        }

        public object ResolveComponent(ResolveRequest request)
        {
            return _container.ResolveComponent(request);
        }
    }
}
