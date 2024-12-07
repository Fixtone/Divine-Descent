# About the Processes NOA Debugger Executes for the Application

Explains various processes that NOA Debugger performs on the application.

## Common

### Processes Executed at the Time of Application Build

It creates a `link.xml` that contains a list of classes that inherit from `DebugCategoryBase` or `NoaCustomMenuBase`.

The generated `link.xml` is deleted after the build is completed, so no difference occurs.

## iOS

### Processes Executed at the Time of Application Build

It sets the following values to be enabled in `Info.plist`:

- `LSSupportsOpeningDocumentsInPlace`: Displays the application's `Documents` folder on the standard **Files**
  application.
- `UIFileSharingEnabled`: Allows the user to share files on the application's `Documents folder.

### Processes Executed on the Application's Runtime

`Application.persistentDataPath` will be excluded from iCloud backup.

## Related Features

- [DebugCommand](./DebugCommand/DebugCommand.md)
- [Adding Custom Menus](./CustomMenu.md)
- [About Download](./Download.md)
