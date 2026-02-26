using System;

public class Recovered
{
	int recPop;
	int recRate;

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
}
