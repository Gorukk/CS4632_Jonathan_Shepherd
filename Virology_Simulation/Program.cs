// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Drawing;

int input = 0;

do
{
    Console.WriteLine("Please enter which simulation you would like to run:\n1. SIRD Compartmental Model\n2. Probability Model");
    input = Convert.ToInt32(Console.ReadLine());
    switch (input)
    {
        case 1:
            SIRD_Model();
            break;
        case 2:
            break;
        default:
            Console.WriteLine("Invalid Input.\nPlease enter the number corresponding to your selection.");
            break;
    }
}while(input != 1 || input != 2);

static void SIRD_Model()
{
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
    int totalChange = 1;
    do
    {
        int deltaInfection = (int)(infPop.getInfRate() * (float)((susPop.getSusPop() * infPop.getInfPop()) / (susPop.getSusPop() + recPop.getRecPop() + infPop.getInfPop())));
        if (deltaInfection > susPop.getSusPop())
        {
            deltaInfection = susPop.getSusPop();
        }
        //int deltaInfection = infPop.Infection(susPop.getSusPop(), recPop.getRecPop(), deadPop.getDeadPop());
        //Console.WriteLine(deltaInfection);

        int deltaDeath = (int)((float)deadPop.getDeadRate() * (float)infPop.getInfPop());
        //int deltaDeath = deadPop.Death(infPop.getInfPop());
        //Console.WriteLine(deltaDeath);

        int deltaRecovery = (int)(recPop.getRecRate() * infPop.getInfPop());
        //int deltaRecovery = recPop.Recovery(infPop.getInfPop());
        //Console.WriteLine(deltaRecovery);

        int deltaReturn = (int)(susPop.getReturnRate() * recPop.getRecPop());
        //int deltaReturn = susPop.Return(recPop.getRecPop());
        //Console.WriteLine(deltaReturn);

        Console.WriteLine("\nIteration: " + iterations);
        susPop.setSusPop(susPop.getSusPop() - deltaInfection + deltaReturn);
        Console.WriteLine("Susceptible Population: " + susPop.getSusPop());

        infPop.setInfPop(infPop.getInfPop() + deltaInfection - deltaDeath - deltaRecovery);
        Console.WriteLine("Infected Population: " + infPop.getInfPop());

        deadPop.setDeadPop(deltaDeath + deadPop.getDeadPop());
        Console.WriteLine("Dead Population: " + deadPop.getDeadPop());

        recPop.setRecPop(recPop.getRecPop() + deltaRecovery - deltaReturn);
        Console.WriteLine("Recovered Population: " + recPop.getRecPop());

        totalChange = deltaDeath + deltaInfection + +deltaRecovery + deltaReturn;
        iterations++;
    } while ((infPop.getInfPop() > 0) && (iterations < 10000) && (totalChange != 0));
    Console.WriteLine("\nSusceptible Population: " + susPop.getSusPop());
    Console.WriteLine("\nInfected Population: " + infPop.getInfPop());
    Console.WriteLine("\nDead Population: " + deadPop.getDeadPop());
    Console.WriteLine("\nRecovered Population: " + recPop.getRecPop());
    Console.WriteLine("\nIterations: " + iterations);

    Console.WriteLine("\nPress Enter to Exit");
    Console.ReadLine();
}