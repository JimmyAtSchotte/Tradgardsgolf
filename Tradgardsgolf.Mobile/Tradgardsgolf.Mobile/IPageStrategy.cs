using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile
{
    public interface IAppPageStrategy
    {
        Page Create(Type type);
    }

    public interface IAppPageFactory
    {
        bool AppliesTo(Type type);

        Page Create();
    }

    public interface IAppPage { }

    public abstract class BaseAppPageFactory<TBaseAppPage> : IAppPageFactory
        where TBaseAppPage : Page
    {
        public bool AppliesTo(Type type)
        {
            return type == typeof(TBaseAppPage);
        }

        public abstract Page Create();
    }

    public class AppPageStrategy : IAppPageStrategy
    {
        private readonly IAppPageFactory[] _factories;

        public AppPageStrategy(IAppPageFactory[] factories)
        {
            _factories = factories;
        }

        public Page Create(Type type)
        {
            return _factories.FirstOrDefault(x => x.AppliesTo(type)).Create();
        }
    }
}
