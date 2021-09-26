# Asset loading
Scenes, prefabs, Resources, StreamingAssets, AssetBundles, Addressables. What to use when and why?

# Problem
There are four ways in Unity to load an asset into memory.
1. `SceneManager.LoadScene`
2. `Resources.Load`
3. `AssetBundle.LoadAsset`
4. `Addressables.LoadAsset`

There are three ways to unload an asset from memory.
1. Load a different scene.
2. `Resources.UnloadUnusedAssets`
3. `AssetBundle.Unload`

The [documentation](https://docs.unity3d.com/Manual/LoadingResourcesatRuntime.html) tells us _what_ those methods do, but it does not tell us which one we should use. There are plenty of [questions](https://forum.unity.com/threads/resource-vs-addressable-for-memory-management.836863/) and discussions all over the internet trying to find these answers, but they either go nowhere or reach a dubious conclusion. How do we know who is right?

# Solution
We can look into Unity's asset memory system in detail using the [memory profiler package](https://docs.unity3d.com/Packages/com.unity.memoryprofiler@0.4/manual/index.html) to find out how things are _really_ working. We can use that knowledge to assess the advantages and disadvantages of the different ways of loading. Given that, we can assess which way is appropriate for our use case.

# Memory profiler
Memory profiler screenshot here.
What is an asset?
What is not an asset?

# Loading

## Scenes
`SceneManager.LoadScene` loads all assets referenced by the scene.
Implicit references.
Recursive dependencies.
Prefabs.
ScriptableObjects.
Asset packing.
Scene bundle.
Asset deduplication.
Shared assets.
Additive scene loading.

## Resources
`Resources.Load` loads an asset in a "Resources" directory by name.
Resources bundle.
Asset deduplication between resources and scenes.
Shared assets.

## StreamingAssets
Has nothing to do with anything, despite its name.
Put your built asset bundles here.

## AssetBundles
`AssetBundle.LoadAsset` loads an asset from an asset bundle.
`AssetBundle.Load` first.
Asset deduplication between bundles.
Asset deduplication between bundles and scenes.
Scene asset bundles.
Asset deduplication between bundles and resources.

## Addressables
`Addressables.LoadAsset` loads an asset by address.
Implicitly loads bundle.
Reference counting.
Handles.
`Addressables.InstantiateAsync`.
Asset deduplication between groups.
Asset deduplication between groups and scenes.
Addressable scenes.
Asset deduplication between groups and resources.
Asset deduplication between groups and bundles.

# Unloading

## Unloading the scene
Load a different scene.
`DontDestroyOnLoad`.
`SceneManager.UnloadScene` does not unload assets.

## Unloading unused assets
`Resources.UnloadUnusedAssets`.
What does "unused" mean?
Addressables vs. `UnloadUnusedAssets`.

## Unloading asset bundles
`AssetBundle.Unload`.
Force vs. no force.
Broken references.
Broken asset links.
Asset duplication in memory.

## Unloading Addressables
Reference counting.
`UnloadAsset`.
`Release`.
Group unloading.
Addressables profiler.

# Analysis
Scenes easiest.
Resources easy. Does not scale.
AssetBundles must die.
Addressables finicky.

# Use cases

## No dynamic content
Use scenes.

## Dynamic loading
Use `Resources`.
4GB limit.
Use scene transitions with loading screen to unload.
Don't roll your own asset library system.

## Streaming
Use Addressables.
Careful with those handles.
Careful with packing.

## Content updates
Hosted asset bundles. Smaller app size. Frequent content updates.
You don't have those. Just update the app including the content.

## Patching
500MB patch limit on Switch.
Asset packing.
Incremental AssetBundles.
Incremental Addressables.

# Further reading

# Translations

- [台灣繁體中文 (zh-TW)](README-zh-TW.md)

If you find this workshop useful and speak another language, I'd very much appreciate any help translating the chapters. Clone the repository, add a localized copy of the README.md, for example README-pt-BR.md, and send me a pull request.
