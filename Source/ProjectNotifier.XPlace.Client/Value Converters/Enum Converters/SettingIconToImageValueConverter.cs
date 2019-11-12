namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// A value converter that takes a <see cref="SettingIcon"/> and returns an image 
    /// </summary>
    public class SettingIconToImageValueConverter : BaseValueConverter<SettingIconToImageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SettingIcon)value)
            {
                case SettingIcon.ApplicationSettings:
                {
                    var resource = DI.GetService<IResourceStore>().GetResource("AppSettingsIcon");
                    return ResourceToBitMap(resource);
                };

                case SettingIcon.UserSettings:
                {
                    // Get the user icon resource 
                    var resource = DI.GetService<IResourceStore>().GetResource("UserSettingIcon");
                    return ResourceToBitMap(resource);
                };

                default:
                {
                    Debugger.Break();
                    return null;
                };
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes a <see cref="ResourceInfo"/> and converts it to a <see cref="BitmapImage"/>
        /// </summary>
        /// <param name="resource"> The resource to convert </param>
        /// <returns></returns>
        private BitmapImage ResourceToBitMap(ResourceInfo resource)
        {
            // Create a bitmap to show the icon
            var image = new BitmapImage();

            // Allow object ui modifications
            image.BeginInit();

            // Add image data
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = resource.ResourceStream;

            // Stop ui modification
            image.EndInit();


            image.Freeze();

            return image;
        }

    };
};