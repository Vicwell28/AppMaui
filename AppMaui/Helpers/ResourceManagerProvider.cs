using System.Globalization;
using System.Reflection;
using System.Resources;

namespace AppMaui.Helpers
{
    public interface IResourceManagerProvider
    {
        ResourceManager Manager { get; }
    }

    public interface IAppDomainProvider
    {
        Assembly[] GetAssemblies();
    }

    public class ResourceManagerProvider : IResourceManagerProvider
    {
        private ResourceManager? manager;
        private readonly IAppDomainProvider? domainProvider;

        public ResourceManager Manager => manager;

        /// <summary>
        /// Crea una instancia de <see cref="ResourceManagerProvider"/>.
        /// </summary>
        /// <param name="domainProvider">Provee una lista de ensamblados en ejecución.</param>
        /// <param name="namespace">El nombre del espacio de nombres que contiene los recursos.</param>
        public ResourceManagerProvider(IAppDomainProvider domainProvider, string @namespace)
        {
            this.domainProvider = domainProvider ?? throw new ArgumentNullException(nameof(domainProvider));
            LoadResources(@namespace);
        }

        private void LoadResources(string @namespace)
        {
            if (string.IsNullOrEmpty(@namespace))
                throw new ArgumentNullException(nameof(@namespace), "El valor proporcionado no puede ser nulo o vacío.");

            var type = domainProvider?.GetAssemblies()
                                     .SelectMany(asm => asm.GetTypes())
                                     .FirstOrDefault(t => t.IsClass && t.FullName == @namespace);

            if (type == null)
                throw new DllNotFoundException($"No se encontró la librería a la que pertenece el tipo '{@namespace}'.");

            manager = new ResourceManager(@namespace, type.Assembly);
        }
    }

    public class AppDomainProvider : IAppDomainProvider
    {
        public Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies();
    }

    public interface ICultureProvider
    {
        string Culture { get; set; }
        string Country { get; }
        string Language { get; }
    }

    public class CultureProvider : ICultureProvider
    {
        private string culture;
        private string country;
        private string language;

        public string Culture
        {
            get => culture;
            set
            {
                culture = value ?? string.Empty;
                var values = culture.Split('-');
                Language = values.FirstOrDefault() ?? string.Empty;
                Country = values.Length > 1 ? values.LastOrDefault() ?? string.Empty : string.Empty;
            }
        }

        public string Country
        {
            get => country;
            private set => country = value;
        }

        public string Language
        {
            get => language;
            private set => language = value;
        }

        public CultureProvider()
        {
            culture = string.Empty;
            language = string.Empty;
            country = string.Empty;
        }
    }

    public interface ITranslationService
    {
        string GetText(string key);
        string this[string key] { get; }
        string GetCurrentCulture();
        void SetResourceManagerCulture(string culture);
    }

    public class ResxTranslationService : ITranslationService
    {
        private readonly IResourceManagerProvider resourceProvider;
        private readonly ICultureProvider cultureProvider;

        public ResxTranslationService(IResourceManagerProvider resourceProvider, ICultureProvider cultureProvider)
        {
            this.resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            this.cultureProvider = cultureProvider ?? throw new ArgumentNullException(nameof(cultureProvider));
        }

        public string GetText(string key)
        {
            SetResourceManagerCulture();
            var text = resourceProvider.Manager.GetString(key);
            return text ?? key;
        }

        public string this[string key] => GetText(key);

        public string GetCurrentCulture() => string.IsNullOrEmpty(cultureProvider.Culture) ? Thread.CurrentThread.CurrentCulture.ToString() : cultureProvider.Culture;

        public void SetResourceManagerCulture(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    var newCulture = new CultureInfo(culture);
                    Thread.CurrentThread.CurrentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;
                }
                catch (CultureNotFoundException)
                {
                    // No se realiza ninguna acción si la cultura no es válida
                }
            }
        }

        private void SetResourceManagerCulture()
        {
            if (!string.IsNullOrEmpty(cultureProvider.Culture))
            {
                try
                {
                    var newCulture = new CultureInfo(cultureProvider.Culture);
                    Thread.CurrentThread.CurrentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;
                }
                catch (CultureNotFoundException)
                {
                    // No se realiza ninguna acción si la cultura no es válida
                }
            }
        }
    }
}