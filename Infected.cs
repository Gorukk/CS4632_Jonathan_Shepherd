using System;

public class Infected
{
	int infPop; //Infected Population
	float infRate; //Rate of susceptible individuals being infected

	public Infected()
	{
		infPop = 100;
		infRate = .25f;
	}

	public Infected(int infPop, float infRate)
	{
		this.infPop = infPop;
		this.infRate = infRate;
	}
}
