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
		int deltaPop = (int)(returnRate * recPop);

		return deltaPop;
	}

    public void setSusPop(int susPop)
    {
        if(susPop > 0)
		{
            this.susPop = susPop;
        }
        else
        {
            this.susPop = 0;
        }
    }

    public int getSusPop()
    {
        return susPop;
    }

    public void setReturnRate(float returnRate)
    {
        this.returnRate = returnRate;
    }

    public float getRecRate()
    {
        return returnRate;
    }

    public void deltaPop(int delta)
    {
        susPop += delta;
        if (susPop < 0)
        {
            susPop = 0;
        }
    }
}
