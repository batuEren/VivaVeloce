# Viva Veloce

Viva Veloce is a retro top-down street racing game. Here is all the code currently used for the demo. Some of it is just boiler plate and not super interesting so I am listing some parts which I think worth looking fown below.

## Table of Contents
- [Racing AI](#racing-ai)
- [Race Position](#race-position)
- [Drag Racing](#drag-racing)


## Racing AI
Racing AI is implemented using a waypoint system that is highly customizable. You can take a look at this [YouTube video](https://www.youtube.com/watch?v=aC5FqIoWeMA&t=42s&ab_channel=YakamozStudios) I made on how it works. 
Check out the RaceAI/CarAIHandler.cs for the code implementation. I have also added some vector computations that are not mentioned in the video for avoiding collisions and using nitro.

## Race Position
The logic implemented for calculating the race position of each car can be found under Racing/Logistics in PositionHandler.cs and CarLapCounter.cs. These scripts allow for the determination of which car is more advanced in the race and can even tell how far apart the cars are.

## Drag Racing
DragRacing/DragRaceCarAcc.cs contains some arcade physics for drag racing. Instead of running a calculation with engine rpm and gear ratios (which, how it originally worked) uses a system to reward the player on how well timed thier shifts are.
Look at this [YouTube video](https://www.youtube.com/watch?v=9Vi99Y8iU6s&ab_channel=YakamozStudios) for more context and detail.
