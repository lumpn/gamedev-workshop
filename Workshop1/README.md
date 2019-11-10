# Workshop #1: Decoupling

Requirements change. Projects grow. Software evolves.

## Problem

Tight coupling between modules of a system means that it's:
- difficult to reason about individual components in isolation.
- difficult to test individual components in isolation.
- difficult to reuse individual components.

All of which make it difficult to understand, maintain, and evolve both individual components and the system as a whole.

![Tangled strings](https://cdn.pixabay.com/photo/2016/06/28/10/49/thread-1484387_640.jpg "Good luck figuring it out.")

## Solution

We want our components to be [modular](https://en.wikipedia.org/wiki/Modular_programming) in order to be able to reason about, test, and reuse each of them individually. Modular components are easier to change or to replace, allowing our system to stay flexible and able to adapt to changing requirements. We achieve modularization by decoupling dependencies and defining minimal interfaces.

![Lego bricks](https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Lego_bricks.jpg/640px-Lego_bricks.jpg "Be LEGO, my friend.")

## Example

Suppose we're making a game like [Diablo](https://en.wikipedia.org/wiki/Diablo_(video_game)) were we have a player character and a UI showing the player's current health and mana. How do we connect the health bar to the player's health value? The two components are on totally different game objects!

### Singletons

We realize in our game there can only be one player character and one UI. That means we can use the [singleton pattern](https://en.wikipedia.org/wiki/Singleton_pattern) to access the other component. So smart!

```csharp
public sealed class Player : MonoBehaviour
{
    public static Player main { get; private set; }
    
    public float maxHealth;
    public float health;

    void Start()
    {
        main = this;
    }
    
    // ...
}
```

```csharp
public sealed class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Update()
    {
        var player = Player.main;
        slider.maxValue = player.maxHealth;
        slider.value = player.health;
    }
}
```

![Health bar](/Workshop1/Documentation/HealthBar.png "Brilliant!")

Done. So easy. Let's move on to the [next task](https://www.reddit.com/r/restofthefuckingowl/). Soon enough we will have added inventory, equipment, items, gems, sockets, talent tree, character stats, spell book, buffs, and enemies to the game, and they all have to talk to each other. So we introduce more singletons.

![Dependency graph](/Workshop1/Documentation/Dependencies.png "Ship it!")

It is then, that our game designer comes up with this new spell which doubles the character attribute effects of each equipped piece of armor and this new berserk buff which times out within 20 seconds unless the player keeps killing enemies. We cry a little on the inside and add more singletons. By the time we get yet another bug report about character stats not resetting properly once a combination of buff times out we're ready to quit our job become baristas instead.

How about let's not do that. What if I told you, each component can be isolated, modular, and still share information with other components? Enter [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection)!

### Dependency injection

So we start reading about [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection), [separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns), and [inversion of control](https://en.wikipedia.org/wiki/Inversion_of_control), and it's a lot of talk and seems really complicated and we have to download extra frameworks to make it work or switch to a new language (hah, hah, *sigh*).

The key insight here is that the [inspector in Unity](https://docs.unity3d.com/Manual/UsingTheInspector.html) *is* a dependency injector. In fact, we've already been using it to inject dependencies. In the example above the `HealthBar` component's reference to the UI `Slider` *is* a dependency that we *injected* using the inspector. Crazy, right? But that's really all there is to dependency injection.

```csharp
public sealed class Player : MonoBehaviour
{
    // Look ma, no singleton!
    // ...
}
```

```csharp
public sealed class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;

    void Update()
    {
        slider.maxValue = player.maxHealth;
        slider.value = player.health;
    }
}
```

![Health bar version 2](/Workshop1/Documentation/HealthBar2.png "Did we gain anything though?")

Okay, so we replaced the singleton with an injected reference to the player component. Great. High fives all around! Except... did we really gain anything? Let's evaluate against our problem definition.

- [x] Reason about individual components in isolation
- [ ] Test individual components in isolation
- [ ] Reuse individual components

Not much improvement, really. We can kind of guess what `HealthBar` does without looking at `Player`, but we can't test it in an empty new scene without also putting a `Player` into the scene. We also can't reuse the code for our `ManaBar`. We will have to duplicate the code and replace every `health` with `mana` to make it work. Disappointing.

Later we want to clean things up a bit by moving our UI into its own scene which gets loaded additively. How do we even connect the `HealthBar` to the `Player` now? They are in different scenes! We're fed up with dependency injection and ready to throw it on the pile of useless ideas that only sound good in theory.

### Scriptable objects

Today's second insight is that we can inject not only references to other components, but also to `ScriptableObject`s, which are [data containers we can use to save data, *independent of class instances*](https://docs.unity3d.com/Manual/class-ScriptableObject.html). Does that mean we can store the player's health independent of the `Player` component? It does! Does that mean we can reference the player's health independent of the `Player` component? It does too! Let's take it for a spin.

TODO scriptable object code

## Refactoring

You don't even have to start over. You can gradually refactor your project to 
