using System;

public class Recovered
{
	int recPop; //Recovered Population
	float recRate; //Rate of Infected recovering from the disease


	public Recovered()
	{
		 recPop = 0;
		 recRate = .25f;
	}

	public Recovered(int recRate)
	{
		recPop = 0;
		this.recRate = recRate;
	}

	public int Recovery(int infPop)
	{
		int deltaPop = recRate * infPop;

		return deltaPop;
	}
}
