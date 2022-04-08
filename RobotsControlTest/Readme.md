# Test task
## Task specification
Design and implement a solution using C# to model control over robots in a bounded area by handling robots' initial coordinates and set of commands. Full text of task see in Task.docx file.

## Architectural design overview
- The solution was implemented as two projects. First one (Model) contains entities of domain area and business logic required to solve the problem. Second one (ConsoleApp) handles user input (i.e. parse and validate), passes data to the Model for processing and then returns the result to the user in required format. This approach helps to separate user interface from business logic, which makes it possible, for example, to change input and/or output data format without modification of Model code. Also, it makes it possible to implement some different types of applications (like, web- or mobile application) using the same business logic.
- The Model provides endpoints to interact with it, but hides its internal mechanisms to avoid impact from outside
- All classes, which represent commands for robots, implement ICommand interface, and each command contains inside logic for its execution. It makes it easier to add support for new kinds of commands in future.
- Fail fast principle was followed during implementation, so exceptions are thrown as soon as something goes wrong

## Assumptions
Some details were not specified in task, so the following assumptions were made:
- Unlimited count of robots can be in the same point simultaneously
- Robots are able to pass through each other
- "Scent" left when a robot moves off the grid, remains forever

## Other comments
- Code style was used, which is close to default for C#, because of its universality. I personally prefer slightly different style
- mstest was chosen for tests because it works "out of the box" and its capabilities are good enough for this task. In a production project I'd rather use nUnit
- Internals of classes are tested with unit tests, which conflicts with "black box testing principle". In my opinion, both approaches are acceptable, but it is a topic for discussion.

## Time spent
It took about 10 hours (net time) to implement the solution from start to end (including writing this readme).