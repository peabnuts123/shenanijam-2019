# Shenanijam 2019 Notes

Links
  - [Podcast Minisode](https://soundcloud.com/butterscotch-shenanigans/ep213)
  - [Durban From Far music](https://soundcloud.com/durbanfromfar)
  - [bfxr sound effect tool](https://www.bfxr.net/)
  - [Shenanijam 2018 - Lessons learned](https://docs.google.com/document/d/1vbl3_RAnChib09h0WrH7WcR3pOIFu8STzsieq8HFh1c/edit#)
  - [Shenanijam 2017 - Lessons learned](https://keep.google.com/u/0/#LIST/1qKUrQxO8LnHbYrGPGPHN3BBYxL8sVYgqU7kTpvG67fBEmjAerp3GChsYkTO5)

Themes
  - Cursemas Eve
  - DNA Chaos
  - Wrenches 101

Maybe feasible achievements
  - Iron Triangle - Your game primarily uses triangles for its art.
  - INNOVATION! - Your game is a battle royale
  - Tis the Season - Your game has a season pass, makes fun of seasons passes, or has seasons
  - Levelhead - Your game lets players build their own levels, maps, etc.
  - Healthy Living - Sleep for 8 hours per night, get an hour of exercise, and only eat healthy food for the duration of the jam.
  - Totally Ethical Surprise Mechanics - The primary means of progressing through the game is lootboxes.
  - Persistent Universe - Something about your game lives online, and every player can affect it.
  - One Punch Man - Your game is controllable with a single input.


Inspiration
  - Trashing on microtransactions and shit
  - Twitch plays pokemon
  - Save data online somewhere
  - SCHENANNYDJAM

Ideas
  - Cursemas Eve
    - You're Santa and you give people Lootboxes which they need to buy keys to open
    - The presents are curses
    - It's the day BEFORE Cursemas ... so ...
    -

  - DNA Chaos
    - Some kind of Gene-splicing mechanic like XKCD's D&D DNA
    - Look BScotch's Goop Legacy from last year
    - You gotta sew together the DNA that's coming apart
      - It's like binding of isaac
      - OR you can merge with the DNA which has affects on your characters abilities ...
      - You gotta collect / rescure DNA but you need to use it in order to achieve this
      - Toss up between consuming all your DNA (and having none left) vs. saving it all (and the game being hard)
      - If you merge with tonnes of DNA you get all drug-fucked
      - It's like a platformer
      -

  - Wrenches 101
    - You gotta collect the 101 missing wrenches
    -


## Design
  - Binding of Isaac style / Zelda style levels
    - Top down, up to 4 exits per room
  - Objective is to gather DNA that's gone missing in the ... catacombs or whatever
  - There's enemies n' stuff in the catacombs
  - The catacombs are procedural
  - Occasionally you find DNA that has random stats
  - You have the choice to combine with the DNA or collect it (your objective)
  - You will need to combine with the DNA to be able to keep surviving
  - You gotta collect as much DNA as you can without dying - but staying alive is hard without consuming the DNA

### Stretch goals
  - After you die your DNA is converted into points
  - Your points can be spent upgrading your guy / save / whatever
  - Gamepad support
  - Your save is synced to Firebase via a generated password

## Mechanics
  - You can move up/down/left/right
  - You have 2 action buttons
    - e.g. Mouse Left/Right or K/L on the keyboard, A/B on the gamepad, etc.
  - Action Button 1 (AB1) is for attack 1
  - Action Button 2 (AB2) is for attack 2
  - AB1 is for "Collect" when overlapping with DNA
  - AB2 is for "Combine" when overlapping with DNA
  - Attack 1 is a small circular attack in front of you
  - Attack 2 is a linear ranged attack e.g. arrow projectiles
  - Your player / DNA has an array of "Genes" which are your stats
  - Genes/Stats
    - Attack 1 strength
    - Attack 1 size
    - Attack 2 rate of fire
    - Attack 2 number of projectiles
    - Player speed
    - Player hitpoints
  - DNA Combining
    - You must combine with the WHOLE STRAND
    - DNA strands are 6 multipliers (one for each gene)
    - Multipliers range from 0.1 to, say, 2.5, normally distributed around, say, 1.2
    - If you combine, the multipliers are applied to your stats, that's it
    -

