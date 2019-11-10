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

We want our components to be [modular](https://en.wikipedia.org/wiki/Modular_programming)
in order to be able to reason about, test, and reuse each of them individually. 
Modular components are easier to change or to replace, allowing our system to stay flexible and able to adapt to changing requirements.
We achieve modularization by decoupling dependencies and defining minimal interfaces.

![Lego bricks](https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Lego_bricks.jpg/640px-Lego_bricks.jpg "Be LEGO, my friend.")

## Example

Suppose we're making a game like [Diablo](https://en.wikipedia.org/wiki/Diablo_(video_game))
were we have a player character and a UI showing the player's current health and mana.
How do we connect the health bar to the player's health value? The two components are on totally different game objects!

### Singletons

We realize in our game there can only be one player character and one UI.
We can use the [singleton pattern](https://en.wikipedia.org/wiki/Singleton_pattern) to access the other component!

```csharp
public sealed class Player : MonoBehaviour
{
    public static Player main { get; private set; }

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

Done. So easy. Let's move on to the [next task](https://www.reddit.com/r/restofthefuckingowl/). Soon enough we will have added inventory, equipment, items, gems, sockets, talent tree, character stats, spell book, buffs, and enemies to the game, and they all have to talk to each other. So you introduce more singletons.

![Dependency graph](/Workshop1/Documentation/Dependencies.png "Ship it!")

It is then, that your game designer comes up with this new spell which doubles the character attribute effects of each equipped piece of armor and this new berserk buff which times out within 20 seconds unless you keep killing enemies. You cry a little on the inside and add more singletons. By the time you get yet another bug report about character stats not resetting properly once a combination of buff times out you're ready to quit your job become a barista instead.

How about let's not do that. What if I told you, each component can be isolated, modular, and still share information with other components? Enter [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection)!

## Dependency injection

So you start reading about [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection), [separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns), and [inversion of control](https://en.wikipedia.org/wiki/Inversion_of_control), and it's all just a lot of talk and seems really complicated and you have to download extra frameworks or switch to a new language (hah, hah, *sigh*).

The key insight here is that the [inspector in Unity](https://docs.unity3d.com/Manual/UsingTheInspector.html) *is* a dependency injector. In fact, you've already been using it to inject dependencies. In the [example above](#singleton) the `HealthBar` component's [reference](#health-bar) to the UI `Slider` *is* a dependency that gets *injected* by Unity at runtime. Crazy, right? But think about it.



## Refactoring

You don't even have to start over. You can gradually refactor your project to 
