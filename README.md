# Shared Values

An inspector based dependency injection system framework for Unity

## Installation
In the package manager go to install package from git URL... and paste the following:

`https://github.com/3NDavis/SharedValues.git?path=./`

## See the wiki for documentation

## What does it do?
Acts as a middle man between components. Settable Via the inspector.
  The components don't care where the values come from as long as they get them.
  
  <img width="425" height="280" alt="image" src="https://github.com/user-attachments/assets/690f5536-18e6-46b3-8b1c-7be53b31d74c" />
  
  **VS**
  
  <img width="425" height="280" alt="image" src="https://github.com/user-attachments/assets/15d6a418-570d-4b85-beb0-de4e109a5b9f" />

In this case the values can also be more easily swapped to add for new interactions between namespaces

### Pros
Disentangles Classes, Components, and Namespaces
Limits Dependencies
Modular
Easily scalable
Easy to create new content
Easy to test
Easy to change


### Cons
Can get difficult to trace a functions path
Can get difficult to debug
Less efficient than the variable of the same type
A lot of scriptable objects can get difficult to organize

## Todos
- QOL
  - `SharedValue = value` and `SharedValueReference = value` conversion for `SharedValue.Value = value`, right now it only works the other way
  - Fix foldouts not saving their collapsed state (there aren't many, curvecontainer is one of these)
  - Setting a value via the sharedValueReference
  - Broadcasting a value when setting the value of a sharedValueReference in the inspector (in play mode)
- Optimization
  - Change SharedValueInstancer Instantiating a scriptable object to something similar but more lightwieght
- Debug Tools
  - list of component listeners on default components
  - More debug logs
- Documentation
  - Github Documentation 
  - Add samples
  - ToolboxComponents Integration 
- Icons
  - Update the way the icons are found per SO
  - Shared Enumerator Icons (list, dictionary)
  - Shared string icons
  - Fix fallback icons
