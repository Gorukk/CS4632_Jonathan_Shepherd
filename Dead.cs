using System;

public class Dead
{
	int deadPop;
	float deathrate;

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
