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

	public Dead(float deathRate)
	{
		deadPop = 0;
		this.deathRate = deathRate;
	}

	public int Death(int infPop)
	{
		int deltaPop = (int)((float)deathRate * (float)infPop);

		return deltaPop;
	}

	public void setDeadPop(int deadPop) {
		if (deadPop > 0) 
		{
            this.deadPop = deadPop;
		}
		else
		{
			this.deadPop = 0;
		}
	}

	public int getDeadPop()
	{
		return deadPop;
	}

    public void setDeadRate(float deathRate)
    {
        this.deathRate = deathRate;
    }

    public float getDeadRate()
    {
        return deathRate;
    }

    public void deltaPop(int delta)
    {
        deadPop += delta;
        if (deadPop < 0)
        {
            deadPop = 0;
        }
    }
}
