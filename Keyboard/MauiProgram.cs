using Microsoft.Extensions.Logging;
using Plugin.Maui.BottomSheet.Hosting;

namespace Keyboard
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBottomSheet()
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
                handler.PlatformView.InputView = new UIKit.UIView();
#endif
            });

            //            // Disable the keyboard for specific Entry controls
            //            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoKeyboardEntry", static (handler, entry) =>
            //            {
            //#if ANDROID
            //                if (entry is CustomNoKeyboardEntry)
            //                {
            //                    handler.PlatformView.ShowSoftInputOnFocus = false;
            //                }
            //#elif IOS
            //                if (entry is CustomNoKeyboardEntry)
            //                {
            //                    handler.PlatformView.InputView = new UIKit.UIView(); // Prevents the keyboard from showing
            //                }
            //#endif
            //            });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

    //// Custom Entry class to identify controls where the keyboard should be disabled
    //public class CustomNoKeyboardEntry : Entry
    //{
    //    public Entry? entTest1;
    //    public Entry? entTest2;
    //    public Entry? entTest3;
    //    public Entry? entTest4;
    //}
}
