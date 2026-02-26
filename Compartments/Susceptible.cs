using System;

public class Susceptible
{
	int susPop; //Susceptible Population
	float returnRate; //Rate of recovered individuals returning to normal population

	public Susceptible(int susPop, float returnRate)
	{
		this.susPop = susPop;
		this.returnRate = returnRate;
	}

	public Susceptible()
	{
		this.susPop = 10000;
		this.returnRate = .10f;
	}

	public int Return(int recPop)
	{
		int deltaPop = returnRate * recPop;

		return deltaPop;
	}
}
