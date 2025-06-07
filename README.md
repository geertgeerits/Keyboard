Custom keyboard for decimal and hexadecimal entry fields

This app is an example and experimental.
It is a custom keyboard that uses a ContentView as overlay page.

Note:
The custom keyboard works fine on an Android device (not always in the simulator) and Windows.
However In iOS when the custom keyboard is enabled, the Entry properties like Selection, Cursor position, Placeholder and Border color, will not showing.
Also 'await scrollView.ScrollToAsync(label, ScrollToPosition.Center, true)' does not work like in Android.
It centers horizontally and vertically for all the Entry controls in iOS even though the Orientation is only set to Vertical.
To use the system keyboard set in the file 'ClassKeyboardMethods.cs' the variable 'bUseCustomKeyboardForIOS' to false (line 10).  In the file 'MauiProgram.cs' comment out 'handler.PlatformView.InputView = [];' (line 28).