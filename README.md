Custom keyboard for decimal and hexadecimal entry fields

This app is an example and experimental.
It is a custom keyboard that uses a custom view that animates from the bottom and overlays content.

Note:
The custom keyboard works fine in Android and Windows. However In iOS when the custom keyboard is enabled, the Entry properties like Selection, Cursor position, Placeholder and Border color, will not showing. To use the system keyboard set in the class ClassKeyboardMethods.cs the variable 'bUseCustomKeyboardForIOS' to false (line10).
