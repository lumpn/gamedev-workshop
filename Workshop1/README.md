# Workshop #1: Decoupling

## Context

Requirements change. Projects grow. Software evolves.

## Problem

Tight coupling between modules of a system means that it's:
- difficult to reason about individual components in isolation.
- difficult to test individual components in isolation.
- difficult to reuse individual components.

All of which make it difficult to understand, maintain, and evolve both individual components and the system as a whole.

![Tangled strings](https://cdn.pixabay.com/photo/2016/06/28/10/49/thread-1484387_960_720.jpg "Good luck figuring it out.")

## Solution

We want our components to be [modular](https://en.wikipedia.org/wiki/Modular_programming)
in order to be able to reason about, test, and reuse each of them individually. 
Modular components are easier to change or to replace, allowing our system to stay flexible and able to adapt to changing requirements.
We achieve modularization by decoupling dependencies and defining minimal interfaces.

![Lego bricks](https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Lego_bricks.jpg/1024px-Lego_bricks.jpg "Be LEGO, my friend.")

## Example

Suppose we're making a game like [Diablo](https://en.wikipedia.org/wiki/Diablo_(video_game)
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

Done. So easy. Let's move on to the [next task](https://www.reddit.com/r/restofthefuckingowl/).

