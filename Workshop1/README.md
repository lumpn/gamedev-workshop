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

The key insight here is that the [inspector in Unity](https://docs.unity3d.com/Manual/UsingTheInspector.html) *is* a dependency injector. In fact, we've already been using it to inject dependencies. In the [example above](#singletons) the `HealthBar` component's reference to the UI `Slider` *is* a dependency that we *injected* using the inspector. Crazy, right? But that's really all there is to dependency injection.

```csharp
public sealed class Player : MonoBehaviour
{
    public float maxHealth;
    public float health;
    
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

Not much improvement, really. We can kind of guess what `HealthBar` does without looking at `Player`, but we can't test it in an empty scene without also putting a `Player` into the scene. We also can't reuse the code for our `ManaBar`. We will have to duplicate the code and replace every `health` with `mana` to make it work. Disappointing.

Later we want to clean things up a bit by moving our UI into its own scene which gets loaded additively. How do we even connect the `HealthBar` to the `Player` now? They are in different scenes! We're fed up with dependency injection and ready to throw it on the pile of useless ideas that only sound good in theory.

### ScriptableObjects

Today's second insight is that we can inject not only references to other components, but also to `ScriptableObject`s, which are [data containers we can use to save data, *independent of class instances*](https://docs.unity3d.com/Manual/class-ScriptableObject.html). Does that mean we can store the player's health independent of the `Player` component? It does! Does that mean we can reference the player's health independent of the `Player` component? It does too! Let's take it for a spin.

```csharp
[CreateAssetMenu]
public sealed class BoundedFloat : ScriptableObject
{
    public float maxValue;
    public float value;
}
```

```csharp
public sealed class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private BoundedFloat data;

    void Update()
    {
        slider.maxValue = data.maxValue;
        slider.value = data.value;
    }
}
```

```csharp
public sealed class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private BoundedFloat health;
    [SerializeField] private BoundedFloat mana;
    
    // ...
}
```

![ScriptableObjects](/Workshop1/Documentation/ScriptableObjects.png "Yes, you can open multiple inspector windows.")

So we moved all the *data* into *data containers* and referenced them from our components. `ScriptableObject`s live in the assets folder and can be referenced from any prefab, any instance, and any scene. We were also able to unify the code for our health bar and our mana bar, because all they really care about is a maximum value and a value. Nothing in UI references our `Player` anymore and vice versa, both just reference *data*. They don't even care whether it's the same data.

![Decoupling](/Workshop1/Documentation/Decoupling.png)

Are we there yet? Let's evaluate again against our problem definition.

- [x] Reason about individual components in isolation
- [x] Test individual components in isolation
- [x] Reuse individual components

Reading the code, we can understand what the `Player` does without reading what the UI's `ProgressBar`s do and vice versa. There is no dependency between the two. We can throw the `Player` into an empty scene, hook it up to some [fake health and mana data](https://en.wikipedia.org/wiki/Mock_object), and run that. No problem. Plain old data is easy to clone.

We can also throw the UI into an empty scene, hook it up to some fake data, and run that. We can even animate the values of our fake data and see how the health and mana bars change. Our UI designer can go nuts prettying it up without ever having to run the actual game. Iterating on UI design has never been faster!

We haven't even started implementing all the other components for our game, but we're already reusing code. We only need one script for both bars and we have a feeling that our `BoundedFloat` will come in handy for lots of other stats and attributes in the future.

## Refactoring

One beautiful thing about dependency injection is that we can apply it to existing projects as well. We don't have to start over. We can gradually carve out individual components by moving data into `ScriptableObject`s and replace direct references between components with references to data step by step. Once we have completely decoupled one component, we can test and refactor it easily in isolation.

## Further reading

- [Unite Austin 2017 - Game Architecture with Scriptable Objects](https://youtu.be/raQ3iHhE_Kk) by @roboryantron
- [Unite 2016 - Overthrowing the MonoBehaviour Tyranny in a Glorious Scriptable Object Revolution](https://youtu.be/6vmRwLYWNRo) by @richard-fine
