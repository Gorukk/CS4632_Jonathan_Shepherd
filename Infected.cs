using System;

public class Infected
{
	int infPop;
	float infRate;

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
