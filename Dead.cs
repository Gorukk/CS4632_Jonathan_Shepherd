using System;

public class Dead
{
	int deadPop; //Amount of people that died from the disease
	float deathrate; //Rate of infected individuals dying

	public Dead()
	{
		deadPop = 0;
		deathrate = .25f;
	}

	public Dead(int deathRate)
	{
		deadPop = 0;
		this.deathrate = deathRate;
	}
}
