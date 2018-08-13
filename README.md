# Embodied Assignment

Thank you for giving me the opportunity to interview with Embodied.me, I had fun working on the above assignment.

* Entity Component System is used as reference for design and architecture of the code.
* Made sure that there is no repeated code and the format is consistent through out te code.
* Naming Convention
  - Pascal casing for naming Methods and Classes
  - Camel casing for naming variables, objects etc.
* To Make sure there is no hard coded data, for example enums, I used class constructor and class fields to create the necessary states.
  - In refactor code, there are two switch cases depended on enum. the switch cases looks like
    ```
    Switch(currentCameraMode.tostring())
    {
    case "FirstPersonMode":
    Do Something....;
    break;
    ...
    ...
    }
    ```
    Using switch cases like this didn't seem right, so I used if cases for now. This can be further improved by using switch case alternative like Dictionary<String, Action>. I looks like..
    ```
    Dictionary<String, Action> switchCaseAlternative = new Dictionary<String, Action>()
    {{Condition, Delegate},....}
    This is the best alternative that I could think for, if and switch case alternative.
    ```
 * For naming the variables, to make it more understandable I used full names to make it more understandable and for some method calls that have long argument list, it is bit difficult to read as we need to scroll side ways, so one way to avoid this is to encapsulate the argument list and call the method with one argument. I was not sure if this is the best way to go, so I have not implemented this in the above code.
 * For TO-DO part of the assignment, Blend 2, Blend 3, Blend 23 ease-in and ease-out can be achieved by cubic Bezier Curve function. So for now I used extern functions to keep the implementation simple.
 
 ### Please take a look at two of my recent project, implemented using Java and C++. I am sure this projects will give you better understanding of my programming skills.
 * [Pomodoro Timer](https://github.com/uday721/PomodoroTimer.git), an android app.
 * [Relationship Compatibility](https://github.com/uday721/RelationshipCompatability.git), the project is used to find the relationship compatibility of two people based on their expenses.
 
 ###### Please review the above submitted code and looking forward to assignment feedback.
    
