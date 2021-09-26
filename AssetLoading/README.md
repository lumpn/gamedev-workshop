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

# Loading / Unloading

## Scenes

## Resources

## StreamingAssets

## AssetBundles

## Addressables

# Use cases

## No dynamic content

## Dynamic loading

## Streaming

# Backlog

Which method of unloading goes with which method of loading? Do they go along? Which assets are currently loaded and why? What do we do when an asset is currently loaded but should not be?



1. `SceneManager.LoadScene` loads all assets referenced by the scene.
2. `Resources.Load` loads an asset in a "Resources" directory by name.
3. `AssetBundle.LoadAsset` loads an asset from an asset bundle.
4. `Addressables.LoadAsset` loads an asset by address.
