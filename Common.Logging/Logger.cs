using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Common.Providers;

namespace Common.Logging
{
    /// <summary>
    /// This is the class that is used to access the logger, through the Provider member
    /// </summary>
    public static class Logger
    {
        #region Private Variables

        // Lock object to ensure safe threads
        internal static readonly object Lock = new object();

        // Only one provider will be used
        private static LogProvider _provider;

        #endregion

        #region Properties

        [Description("Flag indicating whether or not the provider has been initialized")]
        public static bool IsInitialized { get; set; }

        [Description("The instance to access when using this provider")]
        public static LogProvider Instance
        {
            get
            {
                if (!IsInitialized)
                    Initialize();

                return _provider;
            }
        }

        #endregion

        #region Provider Model Magic (the Intialize method)

        [SuppressMessage("ReSharper", "RedundantCatchClause")]
        private static void Initialize()
        {
            // Provide a thread safe provider/providers
            lock (Lock)
            {
                // Do not initialize a provider more than once (Singleton pattern)
                if (!IsInitialized)
                {
                    // Make sure the _provider is still null
                    if (_provider == null)
                    {
                        try
                        {
                            // Get a collection of providers and the default provider.
                            _provider = ProviderFactory.InstantiateDefaultProvider<LogProvider>(@"providers/logging");

                            // Set this feature as initialized
                            IsInitialized = true;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        #endregion
    }
}