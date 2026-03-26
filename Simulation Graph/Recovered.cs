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

	public Recovered(float recRate)
	{
		recPop = 0;
		this.recRate = recRate;
	}

	public int Recovery(int infPop)
	{
		int deltaPop = (int)(recRate * infPop);

		return deltaPop;
	}

    public void setRecPop(int recPop)
    {
		if(recPop > 0)
		{
            this.recPop = recPop;
		}
		else
		{
			this.recPop = 0;
		}
    }

    public int getRecPop()
    {
        return recPop;
    }

    public void setRecRate(float recRate)
    {
        this.recRate = recRate;
    }

    public float getRecRate()
    {
        return recRate;
    }

    public void deltaPop(int delta)
    {
        recPop += delta;
        if (recPop < 0)
        {
           recPop = 0;
        }
    }
}
