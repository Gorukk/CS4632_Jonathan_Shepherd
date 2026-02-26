using System;

public class Dead
{
	int deadPop; //Amount of people that died from the disease
	float deathRate; //Rate of infected individuals dying

	public Dead()
	{
		deadPop = 0;
		deathRate = .25f;
	}

	public Dead(int deathRate)
	{
		deadPop = 0;
		this.deathRate = deathRate;
	}

	public int Death(int infPop)
	{
		int deltaPop = deathRate * infPop;

		deadPop += deltaPop;

		return deadPop;
	}
}
