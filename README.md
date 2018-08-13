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
 * For naming the variables, to make it more understandable I used full names to make it more understandable and for some method calls that have long argument list, it is bit difficult to read as we need to scroll side ways, so one way to avoid this is to encapsulate the argument list
    
