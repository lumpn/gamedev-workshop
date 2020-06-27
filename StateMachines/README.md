# State Machines
Avoid spaghetti code. Everything is a state machine. Decouple state from logic.

# Problem
An object should change its behavior when its internal state changes. That sounds very abstract, but it's a very common pattern in game code. Let's look at some examples.

1. Input handling: In a fighting game, pressing the *Punch* button should throw a different punch depending on whether the player is moving forward, backward, standing still, jumping, or ducking.
2. AI characters: Bots should react differently when spotting the player depending on their current health, weapons, and number of other bots nearby.
3. Game controller: Whether the ability to quit, pause, or save a game is available should depend on whether the game is loading, saving, paused, running, or quitting,

Once you get more familiar with the pattern, you will start seeing it everywhere.

The problem starts when you try to implement a state machine. It quickly looks like spaghetti code.

```csharp
if (Input.GetButton("Punch")) {
    if (player.velocity.z > 0) {
        ForwardPunch();
    } else if (player.velocity.z < 0) {
        Grapple();
    } else if (player.isJumping) {
        if (Input.GetAxis("Vertical") > 0) {
            UpwardPunch();
        } else {
            DownwardPunch();
        }
    } else if (player.isDucking) {
        UpwardPunch();
    } else {
        StandingPunch();
    }
}
```
Not only is the above code hard to understand, it's also incomplete and contains at least one bug.

# Solution
Every branch in your code represents a different state. Draw them on a piece of paper and connect them with arrows representing the state changes.

// TODO Jonas: graphviz
![State machine](https://upload.wikimedia.org/wikipedia/commons/9/9e/Turnstile_state_machine_colored.svg)
- Animator instead of code
- Visualization

# Example
Finite state machines are useful when you have an entity whose behavior changes based on some internal state, that state can be rigidly divided into one of a relatively small number of distinct options, and the entity responds to a series of inputs or events over time.

In games, they are most known for being used in AI, but they are also common in implementations of user input handling, navigating menu screens, parsing text, network protocols, and other asynchronous behavior.

## Jump controller
![Jump controller](./Documentation/JumpController.png "Jump around!")

## Coyote time
![Coyote time](./Documentation/CoyoteTime.png "Meep meep!")

## Double jump
![Double jump](./Documentation/DoubleJump.png "If at first you don't succeed, jump again.")


## References

- [Don’t Re-invent Finite State Machines: How to Repurpose Unity’s Animator](https://medium.com/the-unity-developers-handbook/dont-re-invent-finite-state-machines-how-to-repurpose-unity-s-animator-7c6c421e5785) by Darren Tsung
- [Unite 2015 - Applied Mecanim : Character Animation and Combat State Machines](https://www.youtube.com/watch?v=Is9C4i4XyXk) by Aaron Horne
- [Advanced AI in Unity (made easy) - State Machine Behaviors](https://www.youtube.com/watch?v=dYi-i83sq5g) by Noa Calice
- [Game Programming Patterns - State](http://gameprogrammingpatterns.com/state.html) by Bob Nystrom
