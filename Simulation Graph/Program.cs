using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Simulation_Graph
{
    internal static class Program
    {
        static int runs = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int input = 0;

            do
            {
                Console.WriteLine("Please enter which simulation you would like to run:\n1. SIRD Compartmental Model\n2. Probability Model\n3. Exit");
                input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        SIRD_Model();
                        break;
                    case 2:
                        Prob_Model();
                        break;
                    case 3:
                        Application.Exit();
                        break;
                    default:
                        Console.WriteLine("Invalid Input.\nPlease enter the number corresponding to your selection.");
                        break;
                }
            } while (input != 1 || input != 2);

            void Prob_Model()
            {
                int susPop = 0;
                int infPop = 0;
                int deadPop = 0;
                int recPop = 0;

                List<int> susList = new List<int>();
                List<int> infList = new List<int>();
                List<int> deadList = new List<int>();
                List<int> recList = new List<int>();

                int iterations = 0;

                Console.WriteLine("Enter the percentage chance of infectivity (0% - 100%):");
                float infChance = float.Parse(Console.ReadLine());

                Console.WriteLine("Enter the percentage chance of lethality (0% - 100%):");
                float mortChance = float.Parse(Console.ReadLine());

                int infectionTime = 3;

                char[,] popArray = new char[20, 20];
                int[,] timeArray = new int[20, 20];

                Random rnd = new Random();
                int infectionStartI = rnd.Next(0, 20);
                int infectionStartJ = rnd.Next(0, 20);

                for(int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++) 
                    {
                        if(i == infectionStartI && j == infectionStartJ)
                        {
                            popArray[i, j] = 'I';
                            timeArray[i, j] = 1;
                            infPop++;
                        }
                        else
                        {
                            popArray[i, j] = 'S';
                            timeArray[i, j] = 1;
                            susPop++;
                        }
                    }
                }

                do
                {
                    iterations++;
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            if (popArray[i,j] == 'I')
                            {
                                if(rnd.Next(0,100) <= mortChance)
                                {
                                    popArray[i, j] = 'D';
                                    deadPop++;
                                    infPop--;
                                }
                                else
                                {
                                    timeArray[i,j] = timeArray[i,j] + 1;

                                    if (i-1 >= 0)
                                    {
                                        if(rnd.Next(0,100) <= infChance)
                                        {
                                            popArray[i - 1, j] = 'I';
                                            timeArray[i-1, j] = 1;
                                            infPop++;
                                            susPop--;
                                        }
                                    }

                                    if(i+1 < 20)
                                    {
                                        if (rnd.Next(0,100) <= infChance)
                                        {
                                            popArray[i + 1, j] = 'I';
                                            timeArray[i + 1, j] = 1;
                                            infPop++;
                                            susPop--;
                                        }
                                    }

                                    if(j-1 >= 0)
                                    {
                                        if (rnd.Next(0,100) <= infChance)
                                        {
                                            popArray[i, j - 1] = 'I';
                                            timeArray[i, j - 1] = 1;
                                            infPop++;
                                            susPop--;
                                        }
                                    }

                                    if(j+1 < 20)
                                    {
                                        if (rnd.Next(0,100) <= infChance)
                                        {
                                            popArray[i, j + 1] = 'I';
                                            timeArray[i, j + 1] = 1;
                                            infPop++;
                                            susPop--;
                                        }
                                    }

                                    if (timeArray[i, j] == 3)
                                    {
                                        popArray[i, j] = 'R';
                                        timeArray[i, j] = 0;
                                        infPop--;
                                        recPop++;
                                    }
                                }
                            }else if(popArray[i, j] == 'R')
                            {
                                popArray[i, j] = 'S';
                                recPop--;
                                susPop++;
                            }
                        }
                    }

                    susList.Add(susPop);
                    infList.Add(infPop);
                    recList.Add(recPop);
                    deadList.Add(deadPop);
                } while(infPop > 0 && iterations < 100);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form1 simGraph = new Form1();
                simGraph.addSeries(susList, "Susceptible Population", Color.Red);
                simGraph.addSeries(infList, "Infected Population", Color.Blue);
                simGraph.addSeries(recList, "Recovery Population", Color.Green);
                simGraph.addSeries(deadList, "Dead Population", Color.Black);
                Application.Run(simGraph);

                Bitmap bmp = new Bitmap(simGraph.Width, simGraph.Height, PixelFormat.Format32bppArgb);
                simGraph.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                runs++;
                bmp.Save("ProbabilityRun" + runs, ImageFormat.Png);
            }

            void SIRD_Model()
            {
                int pop;

                float rate;

                List<int> susPopList = new List<int>();
                List<int> infPopList = new List<int>();
                List<int> deadPopList = new List<int>();
                List<int> recPopList = new List<int>();

                Console.WriteLine("Enter the susceptible population:");
                pop = Convert.ToInt32(Console.ReadLine());

                do
                {
                    Console.WriteLine("\nEnter the rate of population returning from Recovery to Susceptible:");
                    rate = (float)Convert.ToDouble(Console.ReadLine());

                    if (rate < 0 || rate > 1.0f)
                    {
                        Console.WriteLine("Value is invalid. Please try again.");
                    }
                } while (rate < 0 || rate > 1.0f);

                Susceptible susPop = new Susceptible(pop, rate);

                Console.WriteLine("\nEnter the starting infected population:");
                pop = Convert.ToInt32(Console.ReadLine());

                do
                {
                    Console.WriteLine("\nEnter the rate of infection:");
                    rate = (float)Convert.ToDouble(Console.ReadLine());

                    if (rate < 0 || rate > 1.0f)
                    {
                        Console.WriteLine("Value is invalid. Please try again.");
                    }
                } while (rate < 0 || rate > 1.0f);

                Infected infPop = new Infected(pop, rate);

                do
                {
                    Console.WriteLine("\nEnter the rate of recovery:");
                    rate = (float)Convert.ToDouble(Console.ReadLine());

                    if (rate < 0 || rate > 1.0f)
                    {
                        Console.WriteLine("Value is invalid. Please try again.");
                    }
                } while (rate < 0 || rate > 1.0f);

                Recovered recPop = new Recovered(rate);

                do
                {
                    Console.WriteLine("\nEnter the rate of mortality:");
                    rate = (float)Convert.ToDouble(Console.ReadLine());

                    if (rate < 0 || rate > 1.0f)
                    {
                        Console.WriteLine("Value is invalid. Please try again.");
                    }
                } while (rate < 0 || rate > 1.0f);

                Dead deadPop = new Dead(rate);

                int iterations = 0;
                int totalChange = 1;
                do
                {
                    int deltaInfection = (int)(infPop.getInfRate() * (float)((susPop.getSusPop() * infPop.getInfPop()) / (susPop.getSusPop() + recPop.getRecPop() + infPop.getInfPop())));
                    if (deltaInfection > susPop.getSusPop())
                    {
                        deltaInfection = susPop.getSusPop();
                    }
                    //int deltaInfection = infPop.Infection(susPop.getSusPop(), recPop.getRecPop(), deadPop.getDeadPop());
                    //Console.WriteLine(deltaInfection);

                    int deltaDeath = (int)((float)deadPop.getDeadRate() * (float)infPop.getInfPop());
                    //int deltaDeath = deadPop.Death(infPop.getInfPop());
                    //Console.WriteLine(deltaDeath);

                    int deltaRecovery = (int)(recPop.getRecRate() * infPop.getInfPop());
                    //int deltaRecovery = recPop.Recovery(infPop.getInfPop());
                    //Console.WriteLine(deltaRecovery);

                    int deltaReturn = (int)(susPop.getReturnRate() * recPop.getRecPop());
                    //int deltaReturn = susPop.Return(recPop.getRecPop());
                    //Console.WriteLine(deltaReturn);

                    //Console.WriteLine("\nIteration: " + iterations);
                    susPop.setSusPop(susPop.getSusPop() - deltaInfection + deltaReturn);
                    //Console.WriteLine("Susceptible Population: " + susPop.getSusPop());

                    infPop.setInfPop(infPop.getInfPop() + deltaInfection - deltaDeath - deltaRecovery);
                    //Console.WriteLine("Infected Population: " + infPop.getInfPop());

                    deadPop.setDeadPop(deltaDeath + deadPop.getDeadPop());
                    //Console.WriteLine("Dead Population: " + deadPop.getDeadPop());

                    recPop.setRecPop(recPop.getRecPop() + deltaRecovery - deltaReturn);
                    //Console.WriteLine("Recovered Population: " + recPop.getRecPop());

                    susPopList.Add(susPop.getSusPop());
                    infPopList.Add(infPop.getInfPop());
                    deadPopList.Add(deadPop.getDeadPop());
                    recPopList.Add(recPop.getRecPop());

                    totalChange = deltaDeath + deltaInfection + +deltaRecovery + deltaReturn;
                    iterations++;
                } while ((infPop.getInfPop() > 0) && (iterations < 10000) && (totalChange != 0));
                Console.WriteLine("\nSusceptible Population: " + susPop.getSusPop());
                Console.WriteLine("\nInfected Population: " + infPop.getInfPop());
                Console.WriteLine("\nDead Population: " + deadPop.getDeadPop());
                Console.WriteLine("\nRecovered Population: " + recPop.getRecPop());
                Console.WriteLine("\nIterations: " + iterations);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form1 simGraph = new Form1();
                simGraph.addSeries(susPopList, "Susceptible Population", Color.Red);
                simGraph.addSeries(infPopList, "Infected Population", Color.Blue);
                simGraph.addSeries(recPopList, "Recovery Population", Color.Green);
                simGraph.addSeries(deadPopList, "Dead Population", Color.Black);
                Application.Run(simGraph);
                runs++;
                Bitmap bmp = new Bitmap(simGraph.Width, simGraph.Height, PixelFormat.Format32bppArgb);
                simGraph.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                runs++;
                bmp.Save("ProbabilityRun" + runs, ImageFormat.Png);

                Console.WriteLine("\nPress Enter to Continue");
                Console.ReadLine();
            }
        }
    }
}
