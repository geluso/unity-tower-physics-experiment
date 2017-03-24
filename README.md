# Jenga Plank Tower Physics Expirement
This project uses a world script to generate a jenga tower from a plank prefab.
The world scene starts with one plank placed as a "reference origin" plank. The
script takes a reference to the origin plank and the plank prefab. The script
generates a tower with a specified height. Each row of the tower contains three
planks, like a Jenga tower. Each plank has width 1 and length 5. There's a 1
unit gap between each plank so the entire structure looks like |||.

Each row is rotated 90 degrees from the row below it, like a Jenga tower.

This project was built to test how stable the built in Unity physics engine is.
I was curious to see how stable a tower is as planks are stacked higher and
higher.

Previous experiments with towers would fall down very easy, and planks had a
tendency to "fall down" into place when the scene starts because they were
placed too far away from each other with air gaps between surfaces. Other
towers would furiously explode because planks were placed inside each other.

The purpose of this project was to specifically write a script that would
place the planks as exactly together as possible. I thought if I could place
the planks together perfectly with the script then the scene would start with
a perfectly still tower.

My assumption was incorrect. Although I think the script arranges the planks 
perfectly the entire tower still "falls down" and squishes in on itself when
the scene first starts.

I also tried editing the physics settings for the project. Increasing the
**sleep threshold** was the most successful tweak. Sleep Threshold is .005
by default. Changing it to 1 actually made even 160 tall giant towers stand
in the beginning!

* Setting **gravity** to zero absolutely eliminates the initial tower drop down
  and settle in effect. Pieces just hang there in mid air. There's no cannon
  in this experiment, but it's unlikely zero gravity would be a good thing.
* **Enable Adaptive Forces** had no observable effect.
* changing **bounce threshold** had no real effect.
* changing **default contact offset** had no real effect.

# Conclusion
The built-in Unity physics engine is no good for building towers like this. We
can't rely on just physics to have semi-stable towers. The towers wiggle too
much and are too fragile. 

## Recommending Leo's Solution
If we continue to use the built-in Unity physics engine I still like Leo's
implementation where we start every object as static and only enable physics
if the object is within the bounds of some explosion sphere. This allows us
to make extremely rigid towers that can have entire chunks blown out of them
and maintain their structure.

A consequence of Leo's initial design is the tower can end up with floating
islands that aren't connected to anything beneath them. We'd have to continue
to write our own logic to make these pieces crumble and fall when we detect
that they're not connected to the rest of the tower any more.

## An Idea for Improvement
Additionally, I'd like to add certain special "connection" points inside
our physics-enabled tower. We can group the tower into multiple sections
and define certain crucial keystone blocks. When the keystone blocks are
severed we can have that trigger more explosions or enable more physics
for other blocks in the tower.

At this point I truly think this is the way to go:
* Start blocks as static, with physics disabled so they stay exactly where
  they start.
* Enable physics when blocks are "hit" in some bounding collision area
* Make certain pieces of the tower special "keystone" pieces that trigger
  the activation of regular physics for other pieces when they're hit.
