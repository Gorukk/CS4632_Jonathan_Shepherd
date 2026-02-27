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

	public int Infection(int susPop, int recPop)
	{
		int deltaPop = (int)(infRate * ((susPop * infPop) / (susPop + recPop + infPop)));

		return infPop;
	}

    public void setInfPop(int infPop)
    {
        if(infPop > 0)
		{
            this.infPop = infPop;
        }
        else
        {
            this.infPop = 0;
        }
    }

    public int getInfPop()
    {
        return infPop;
    }

    public void setInfRate(float infRate)
    {
        this.infRate = infRate;
    }

    public float getInfRate()
    {
        return infRate;
    }
}
