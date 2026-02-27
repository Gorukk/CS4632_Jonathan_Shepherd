# CS4632_Jonathan_Shepherd
The domain of this simulation is virology. The main scope is how lethality affects virus transmission. The only factors considered will be the rate of infectivity, the rate of recovery, and the rate of death.

The main focus will be a compartmental model based of the SIR model. The compartments will be Susceptible, Infected, Recovered, and Dead. Movement between the compartments will be covered by probabilistic equations. 

For simplification the population will start with a percentage of the population sick, between 5 - 10 percent. The population will not grow and the only deaths will be due to infection. Recovered individuals will not be gradually returned to the Susceptible population. Dead population will not be vectors of transmission.

C# will be the programming language used for this project. It will be coded using the Visual Studio IDE. A discrete-event model will be used. The main metric recorded will be deaths per infected.

-Project Status
Currently implemented is the basic compartment model and the equations for it. A basic command line menu is used to enter calues as well as to output values. A better command line menu will be implemented next. After that a seperate probability model wil also be available in the future

-Usage
To run 
