# Launching Tool

![start-up-tool](../img/start-up-tool.png)

The tool starts when you press [![noa-debugger-logo](../img/icon/noa-debugger-logo.png)] button.

This start button is located at the bottom left of the screen when the application starts, and you can adjust its
position by dragging.

The position of the start button is saved separately in portrait and landscape screens, and if the saved value is off
the screen, it is relocated to the bottom left.

**Note:** If the SortOrder is set to 1000 or more in a Canvas within the application, the UI of NOA Debugger may not
come to the front. If you want to change the SortOrder of the Canvas, please refer to the [Tool Settings](./Settings.md)
and set the Canvas's SortOrder.

## Hiding All UIs

When you press and hold the launch button and then release it, the floating windows and the launch button will be
hidden, and when you press the position where the launch button was originally displayed, it will return to its original
state.

Please refer to [About the Floating Windows](./FloatingWindow.md) for details about the floating windows.

## Operation When an Error Occurs in the Application

When a Unity error or API error occurs, the launch button of the tool blinks red for a few seconds to alert you. After
that, it continues to light up in red until it displays the function that detected the error.

If you had hidden the launch button of the tool, the launch button will be hidden after blinking red for a few seconds.

The notification settings for error output can be changed from the NOA Debugger Editor. Please refer to
the [Tool Settings](./Settings.md) for more details.

## Operation When Not Meeting the System Requirements

If you do not meet the system requirements, the text of **NOA Debugger** in the top left of the window turns orange.

![tool-window-system-not-supported](../img/tool-window-system-not-supported.png)
