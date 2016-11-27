using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

using JoycePrint.UI;

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

            RemoveScripts(content);

            foreach (var virtualPath in bundle.VirtualPaths)
            {
                // Stop the script from being added twice
                if (content.Contains(virtualPath)) continue;
                _bundle.Include(virtualPath);
            }

            return this;
        }

        /// <summary>
        /// TODO: figure out how to remove a file from a bundle
        /// we cant remove and readd the bundle as that's done on app start
        /// we can remove a file from the bundle though - IS THIS POSSIBLE
        /// 
        /// TODO: The scripts are not getting removed and static methods are being used where we need instanced ones
        /// should i be changing the bundle like this or can it effect another user
        /// even if i make everything non static, isn't there only 1 bundle for each web app
        ///     ie the bundle is shared between all instances of the site - so changing one could screw someone else up?????????????
        /// </summary>
        /// <param name="content"></param>
        private void RemoveScripts(List<string> content)
        {
            foreach (var file in content)
            {
                if (!BundleConfig.BaseBundle.Contains(file))
                {
                    
                    //content.Remove(file);
                    var bundlefiles = _bundle;
                }
            }
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