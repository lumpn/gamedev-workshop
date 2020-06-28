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

![State machine](./Documentation/StateMachine.png "Looks familiar?")

 The hard part is making sure all the state changes are in the right place and getting triggered correctly, but that's independent from implementing what the *Punch* button is doing in each state.

Okay, so now that we know the *shape* of the problem, how do we implement it?

## Animators
The graph above looks a lot like an [animator controller](https://docs.unity3d.com/Manual/Animator.html) in Unity. And that's exactly what we are going to use, because it turns out an animator controller can do so much more than just playing a bunch of animations. Let's walk through an example to see how it works.

# Example
Suppose we are implementing input handling for a Jump 'n' Run game. 


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

# Translations
- [台灣繁體中文 (zh-TW)](README-zh-TW.md)

If you find this workshop useful and speak another language, I'd very much appreciate any help translating the chapters. Clone the repository, add a localized copy of the README.md, for example README-pt-BR.md, and send me a pull request.
