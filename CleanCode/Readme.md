# Clean code for Unity
An attempt to list what works well and what doesn't. Mostly scars.

# Platform specific code
```csharp
#if UNITY_PS4
```
```csharp
if (Application.isConsolePlatform)
```
Avoid platform specific code. If you absolutely must have platform specific code, because you need call APIs only available on that platform, build a platform abstraction layer. Chances are other platforms will have a very similar feature under a slightly different API. Aim to expose a unified cross platform API that hides all the platform specific calls away. Use `asmdef`s instead of preprocessor directives.

# Editor only code
```csharp
#if UNITY_EDITOR
```
```csharp
if (Application.isEditor)
```
Avoid editor only code in runtime scripts. Put all your editor code in `Editor` folders so that it doesn't get compiled into the standalone player. No exceptions!

# Play mode only code
```csharp
if (Application.isPlaying)
```
Put runtime code in runtime scripts. Put editor code in `Editor` scripts.

# Edit mode only code
```csharp
if (!Application.isPlaying)
```
Put runtime code in runtime scripts. Put editor code in `Editor` scripts. Seriously.

# Execute in edit mode
Just no. Use `[ContextMenu]` if you need to run a runtime function in edit mode. But do you really? Think long and hard about moving the code into a proper editor window.

# Mutable static data
Debugging nightmare. Use `ScriptableObjects` instead.

# Singletons
Dependency hell. Use `ScriptableObjects` instead.

# Formatting
No tabs. Indent using four spaces. No trailing spaces. Unix style line endings. Use ASCII characters only. Use exactly one empty new line at the end of the file. All of this is to reduce noise when looking at diffs or resolving merge conflicts.

# Canonical file format
If you produce any kind of custom files where order is not important, sort the data before writing to disk. If it's case-insensitive strings, convert all of them to lower case before writing to disk. It will make diffs and merge conflicts much easier to deal with.

# ignore.conf
Keep it sorted alphabetically. See canonical file format.
