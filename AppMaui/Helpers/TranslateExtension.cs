using AppMaui.Helpers;

namespace AppMaui.MarkupExtensions
{
	[ContentProperty(nameof(Key))]
	public class TranslateExtension : IMarkupExtension<string>
	{
		public string Key { get; set; } = string.Empty;

		public string ProvideValue(IServiceProvider serviceProvider)
		{
			if (Key == null)
				return string.Empty;

			var translationServiceFromDI = IPlatformApplication.Current?.Services?.GetService<ITranslationService>();

			var translation = translationServiceFromDI != null ? translationServiceFromDI.GetText(Key) : $"!{Key}!";

			return translation;
		}

		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		{
			return ProvideValue(serviceProvider);
		}
	}
}
