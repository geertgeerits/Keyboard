using Microsoft.Extensions.Logging;

namespace Keyboard
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Disable the keyboard for all Entry controls
//            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoKeyboardEntry", static (handler, entry) =>
//            {
//#if ANDROID
//                handler.PlatformView.ShowSoftInputOnFocus = false;
//#elif IOS
//                handler.PlatformView.InputView = [];                // Hide keyboard
//                handler.PlatformView.InputAccessoryView = null;     // Hide accessory bar ('Done' key)
//#endif
//            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
