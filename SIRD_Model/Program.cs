// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

int pop;

float rate;

Console.WriteLine("Enter the susceptible population:");
pop = Convert.ToInt32(Console.ReadLine());

do
{
    Console.WriteLine("\nEnter the rate of population returning from Recovery to Susceptible:");
    rate = (float)Convert.ToDouble(Console.ReadLine());

    if (rate < 0 || rate > 1.0f)
    {
        Console.WriteLine("Value is invalid. Please try again.");
    }
} while (rate < 0 || rate > 1.0f);

Susceptible susPop = new Susceptible(pop, rate);

Console.WriteLine("\nEnter the starting infected population:");
pop = Convert.ToInt32(Console.ReadLine());

do
{
    Console.WriteLine("\nEnter the rate of infection:");
    rate = (float)Convert.ToDouble(Console.ReadLine());

    if (rate < 0 || rate > 1.0f)
    {
        Console.WriteLine("Value is invalid. Please try again.");
    }
} while (rate < 0 || rate > 1.0f);

Infected infPop = new Infected(pop, rate);

do
{
    Console.WriteLine("\nEnter the rate of recovery:");
    rate = (float)Convert.ToDouble(Console.ReadLine());

    if (rate < 0 || rate > 1.0f)
    {
        Console.WriteLine("Value is invalid. Please try again.");
    }
} while (rate < 0 || rate > 1.0f);

Recovered recPop = new Recovered(rate);

do
{
    Console.WriteLine("\nEnter the rate of mortality:");
    rate = (float)Convert.ToDouble(Console.ReadLine());

    if (rate < 0 || rate > 1.0f)
    {
        Console.WriteLine("Value is invalid. Please try again.");
    }
} while (rate < 0 || rate > 1.0f);

Dead deadPop = new Dead(rate);

int iterations = 0;

do
{
    int deltaInfection = infPop.Infection(susPop.getSusPop(), recPop.getRecPop());
    //Console.WriteLine(deltaInfection);

    int deltaDeath = deadPop.Death(infPop.getInfPop());
    //Console.WriteLine(deltaDeath);

    int deltaRecovery = recPop.Recovery(infPop.getInfPop());
    //Console.WriteLine(deltaRecovery);

    int deltaReturn = susPop.Return(recPop.getRecPop());
    //Console.WriteLine(deltaReturn);

    susPop.setSusPop(susPop.getSusPop() - deltaInfection + deltaReturn);
    Console.WriteLine(susPop.getSusPop());

    infPop.setInfPop(infPop.getInfPop() + deltaInfection - deltaDeath - deltaRecovery);
    Console.WriteLine(infPop.getInfPop());

    deadPop.setDeadPop(deltaDeath + deadPop.getDeadPop());
    Console.WriteLine(deadPop.getDeadPop());

    recPop.setRecPop(recPop.getRecPop() + deltaRecovery - deltaReturn);
    Console.WriteLine(recPop.getRecPop());

    iterations++;
}while((infPop.getInfPop() > 0) && (iterations < 10000));
Console.WriteLine("\nSusceptible Population: " + susPop.getSusPop());
Console.WriteLine("\nInfected Population: " + infPop.getInfPop());
Console.WriteLine("\nDead Population: "  + deadPop.getDeadPop());
Console.WriteLine("\nRecovered Population: " + recPop.getRecPop());
Console.WriteLine("\nIterations: " + iterations);

Console.WriteLine("\nPress Enter to Exit");
Console.ReadLine();
