using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace JoycePrint.Web.Extensions
{
    public class ComposableBundle<T> where T : Bundle
    {
        private T _bundle;
        private List<string> _virtualPaths = new List<string>();

        public ComposableBundle(T bundle)
        {
            _bundle = bundle;
        }

        public string[] VirtualPaths
        {
            get { return _virtualPaths.ToArray(); }
        }

        public T Bundle
        {
            get { return _bundle; }
        }

        public ComposableBundle<T> Include(params string[] virtualPaths)
        {
            _virtualPaths.AddRange(virtualPaths);
            _bundle.Include(virtualPaths);
            return this;
        }

        public ComposableBundle<T> UseBundle(ComposableBundle<T> bundle)
        {
            var collection = new BundleCollection();
            collection.Add(_bundle);

            var resolver = new BundleResolver(collection);
            List<string> content = resolver.GetBundleContents(_bundle.Path)?.ToList();

            foreach (var virtualPath in bundle.VirtualPaths)
            {
                if (content.Contains(virtualPath)) continue;
                _bundle.Include(virtualPath);
            }

            return this;
        }
    }

    public static class BundleExtensions
    {
        public static ComposableBundle<T> AsComposable<T>(this T bundle) where T : Bundle
        {
            return new ComposableBundle<T>(bundle);
        }

        public static ComposableBundle<T> Add<T>(this BundleCollection bundles, ComposableBundle<T> bundle) where T : Bundle
        {
            bundles.Add(bundle.Bundle);
            return bundle;
        }
    }
}