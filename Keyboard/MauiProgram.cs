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
            // If you need to re-enable the keyboard later, you can reset the InputView to null on iOS or set ShowSoftInputOnFocus to true on Android
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoKeyboardEntry", static (handler, entry) =>
            {
#if ANDROID
                handler.PlatformView.ShowSoftInputOnFocus = false;
#elif IOS
                // !!!BUG!!!? When the system keyboard is disabled,
                // the Entry properties like Selection, Cursor position, Placeholder and Border color, will not showing
                // Also uncomment the next line to disable the system keyboard
                handler.PlatformView.InputView = [];

                // Instead of assigning a blank UIView, assign null to InputView
                // This disables the system keyboard but keeps selection and cursor working (does not work !!!)
                //handler.PlatformView.InputView = null;
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
