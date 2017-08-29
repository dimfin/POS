using POS.DataProvider;
using System.Data.Entity;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace POS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new EntitiesContextInitializer());

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage("ru-RU")));

            base.OnStartup(e);
        }
    }
}
