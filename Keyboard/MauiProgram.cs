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

            /* Disables the system software keyboard for all Entry controls in the application.
               After using this method, Entry controls will no longer display the system keyboard when focused on supported platforms.
               This change applies globally and may affect user input scenarios that rely on the system software keyboard.

               If you need the system software keyboard (works for Android and Windows, not for iOS) for some entry controls
               put these in the 'Focused event' of the entry control:
               'ClassKeyboardMethods.HideBottomSheet(...)' and 'entry.ShowSoftInputAsync(...)'.
               In the 'Unfocused event' of the entry control call 'entry.HideSoftInputAsync(...)' and ClassKeyboardMethods.ShowBottomSheet().

               Re-enabling the system keyboard for all entry controls: set ShowSoftInputOnFocus to true on Android and reset the InputView to null on iOS.
               However, it is not recommended to turn the system keyboard back on after disabling it, as this may not produce the desired result.
            */
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoKeyboardEntry", static (handler, entry) =>
            {
#if ANDROID
                handler.PlatformView.ShowSoftInputOnFocus = false;
#elif IOS
                handler.PlatformView.InputView = [];                // Hide keyboard
                handler.PlatformView.InputAccessoryView = null;     // Hide accessory bar ('Done' key)
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
