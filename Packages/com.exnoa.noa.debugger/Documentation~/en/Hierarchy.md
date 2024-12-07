# Hierarchy

Displays the hierarchy structure and component status in the current scene.

## Screen Layout

![hierarchy](../img/hierarchy/hierarchy.png)

### 1. Refresh Button

Pressing the [![reset](../img/icon/reset.png)] button retains the information at the time of press.

If information was already being retained, it will be overwritten with the information at the time of press.

### 2. Number of Scenes

Displays the number of scenes in the hierarchy.

### 3. Number of Objects

Displays the number of objects in the hierarchy.

### 4. Hierarchy Structure

Displays the acquired hierarchy structure.

- Pressing the [![tree-close](../img/icon/tree-close.png)] icon expands the tree.
- Pressing the [![tree-open](../img/icon/tree-open.png)] icon closes the tree.

Inactive objects are displayed in gray.

By selecting the object name, the component information is displayed in the object information described later.

### 5. Lock Button

Keeps the display of the selected object.

### 6. Object Active/Inactive Button

Toggles the active/inactive state of the selected object.

### 7. Number of Components

Displays the number of components attached to the selected object.

### 8. Components in Object

Displays the component information owned by the selected object.

By pressing the [![tree-close](../img/icon/tree-close.png)] icon, you can expand the component information.

The fields displayed as component information are either public, or non-public and have the SerializeField attribute.

**Note:** Fields and properties of components provided by UnityEngine are displayed. Fields of arrays or
structures/classes with the Serializable attribute can be further expanded by pressing
the [![tree-close](../img/icon/tree-close.png)]icon.

**Note:** Structures/classes of Generic types other than List are not displayed.

You can display the information by pressing the [![tree-open](../img/icon/tree-open.png)] icon.

Also, the default hierarchy display of the structure is up to 3 levels. If you want to change the number of hierarchy
displays, please change it from [Tool Settings](./Settings.md).
