
# Enemy Behavior

### Darting Beetle Behavior

Public fields
* Speed
* Dart Time
* Pause Time

Darting Beetle rotates for *Pause Time* at a fixed angle.  Then it darts forward for *Dart Time* at speed *Speed*.  Then it rotates to the other fixed angle and darts forward.

### Mole Behavior

Public Fields
* Mole State (Rest, Raise, Lower)
* Vertical Speed
* Time Interval
* Start Position
* Random Circle Radius

The mole raises for a fixed *time interval*, lowers for the fixed *time interval*, then rests for a fixed *Time Interval*.
It then randomly picks a new location within a circle centered around the *start position*, with radius *Random Circle Radius*.

### Bunny Behavior

Public Fields
* --On Ground Distance--
* Is Jumping
* Is Grounded
* Hop Cycle
* Hop Left



