using System;
using System.IO;

/*static int Fatorial(int n)
{
    int resultado = n;
    while (n > 0)
    {
        resultado = resultado * n;
        n--;
    }   
    return resultado;
}*/
public class Corpo
{
    private string Nome;
    private float Massa;
    private float Raio;
    private float PosX;
    private float PosY;
    private float VelX;
    private float VelY;

    //atributos adicionais
    private double[,] Força;
    private float[,] Direção;
    private double[,] ForçaXY;
    private double[,] ForçaResultante;
    private double[,] Aceleração;

    //


    public Corpo()
    {
        Nome = " ";
        Massa = 0;
        Raio = 0;
        PosX = 0;
        PosY = 0;
        VelX = 0;
        VelY = 0;
    }
    public Corpo(string n, float m, float r, float x, float y, float vx, float vy)
    {
        Nome = n;
        Massa = m;
        Raio = r;
        PosX = x;
        PosY = y;
        VelX = vx;
        VelY = vy;
    }
    public string getNome()
    {
        return Nome;
    }
    public void setNome(string n)
    {
       Nome=n;
    }
    public float getMassa()
    {
        return Massa;
    }
    public float getPosX()
    {
        return PosX;
    }
    public float getPosY()
    {
        return PosY;
    }
    public void setPosX(float x)
    {
        PosX = x;
    }
    public void setPosY(float y)
    {
        PosX = y;
    }
    public float getVelX()
    {
        return VelX;
    }
    public float getVelY()
    {
        return VelY;
    }
    public void setVelX(float x)
    {
        VelX = x;
    }
    public void setVelY(float y)
    {
        VelY = y;
    }


    public void Interações(int l)
    {
        Força = new double[l, 2];
        ForçaXY = new double[l, 2];
        Direção = new float[l, 2];
        ForçaResultante = new double[1, 3];
        Aceleração = new double[1, 2];


        for (int i = 0; i < l; i++)
        {
            Força[i, 0] = 0;
        }
    }

    public void setForçaDir(double f, float x, float y)
    {
        for (int i = 0; i < Força.Length; i++)
        {
            if (Força[i, 0] == 0)
            {
                Força[i, 0] = f;
                Direção[i, 0] = x;
                Direção[i, 1] = y;
                i = Força.Length;
            }
        }
    }
    public double getForça(int i)
    {
        return Força[i, 0];
    }
    public float getDirX(int i)
    {
        return Direção[i, 0];
    }
    public float getDirY(int i)
    {
        return Direção[i, 1];
    }

    public void setAngulo(int i, double a)
    {
        Força[i, 1] = a;
    }
    public double getAngulo(int i)
    {
        if (Força[i, 1] < 0)
        {
            Força[i, 1] = Força[i, 1] * -1;
        }
        return Força[i, 1];
    }
    public void setForçaX(int i, double fx)
    {
        ForçaXY[i, 0] = fx;
    }
    public void setForçaY(int i, double fy)
    {
        ForçaXY[i, 1] = fy;
    }
    public double getForçaX(int i)
    {
        return ForçaXY[i, 0];
    }
    public double getForçaY(int i)
    {
        return ForçaXY[i, 1];
    }
    public void setForçaResultante(double fx, double fy, double fr)
    {
        ForçaXY[1, 0] = fx;
        ForçaXY[1, 1] = fy;
        ForçaXY[1, 2] = fr;
    }
    public void setAceleraçãoX(double x)
    {
        Aceleração[0,0] = x;
    }
    public double getAceleraçãoX()
    {
        return Aceleração[0, 0];
    }
    public void setAceleraçãoY(double y)
    {
        Aceleração[0, 1] = y;
    }
    public double getAceleraçãoY()
    {
        return Aceleração[0, 1];
    }
    public void log()
    {
        Console.WriteLine("<{0}>;<{0}>;<{0}>;<{0}>;<{0}>;<{0}>;<{0}>;",Nome,Massa,Raio,PosX,PosY,VelX,VelY);
    }


}


public class Universo
{
    private int qntCorpos;
    private int qntIteracoes;
    private float tIteracao;
    Corpo[] CorposCelestes;
    int CorposOcupados = 0;
    public Universo(int c, int q, float t)
    {
        CorposCelestes = new Corpo[c];
        qntIteracoes = q;
        tIteracao = t;
        qntCorpos = c;
    }

    public void AddCorpo(Corpo k)
    {
        CorposCelestes[CorposOcupados] = k;
        CorposOcupados++;
    }

    public void Simulacao()
    {
        double f;
        double G = Math.Pow(6.674184, -11);


        // Calculando a força de cada corpo em relação a todos os outros corpos
        for (int i = 0; i < qntCorpos; i++)
        {
            for (int j = i + 1; j < qntCorpos; j++)
            {
                double Distancia = Math.Sqrt(Math.Pow(CorposCelestes[i].getPosX() - CorposCelestes[j].getPosX(), 2.0) - Math.Pow(CorposCelestes[i].getPosY() - CorposCelestes[j].getPosY(), 2.0));
                f = G * (CorposCelestes[i].getMassa() * CorposCelestes[j].getMassa()) / Distancia;
                CorposCelestes[i].setForçaDir(f, CorposCelestes[j].getPosX(), CorposCelestes[j].getPosY());
                CorposCelestes[j].setForçaDir(f, CorposCelestes[i].getPosX(), CorposCelestes[i].getPosY());

            }
        }

        // Calculando a força vetorial resultante
        for (int i = 0; i < qntCorpos; i++)
        {
            for (int j = 0; j < qntCorpos; j++)
            {
                CorposCelestes[i].setAngulo(i, Math.Atan((CorposCelestes[i].getDirY(j) - CorposCelestes[i].getPosY()) / (CorposCelestes[i].getDirX(j) - CorposCelestes[i].getPosX())));
                CorposCelestes[i].setForçaX(j, CorposCelestes[i].getForça(j) * Math.Cos(CorposCelestes[i].getAngulo(i)));
                CorposCelestes[i].setForçaY(j, CorposCelestes[i].getForça(j) * Math.Sin(CorposCelestes[i].getAngulo(i)));
                if ((CorposCelestes[i].getDirY(j) - CorposCelestes[i].getPosY()) < 0)
                {
                    CorposCelestes[i].setForçaY(j, CorposCelestes[i].getForçaY(j) * -1);
                }
                if ((CorposCelestes[i].getDirX(j) - CorposCelestes[i].getPosX()) < 0)
                {
                    CorposCelestes[i].setForçaX(j, CorposCelestes[i].getForçaY(j) * -1);
                }
            }
        }
        //Soma das forças X,Y,calculo da força resultante entre elas e das acelerações consequentes
        for (int i = 0; i < qntCorpos; i++)
        {
            double fx = 0;
            double fy = 0;
            double fr = 0;
            for (int j = 0; j < qntCorpos; j++)
            {
                fx += CorposCelestes[i].getForçaX(j);
                fy += CorposCelestes[i].getForçaY(j);
            }
            fr = Math.Sqrt(Math.Pow(fx, 2) + Math.Pow(fy, 2));
            CorposCelestes[i].setForçaResultante(fx, fy, fr);
            CorposCelestes[i].setAceleraçãoX(fx / CorposCelestes[i].getMassa());
            CorposCelestes[i].setAceleraçãoX(fy / CorposCelestes[i].getMassa());
        }
        // Formula de posição 
        for (int i = 0; i < qntCorpos; i++)
        {
            CorposCelestes[i].setPosX((float)(CorposCelestes[i].getPosX() + CorposCelestes[i].getVelX() * tIteracao + (CorposCelestes[i].getAceleraçãoX() * Math.Pow(tIteracao, 2)) / 2));
            CorposCelestes[i].setPosY((float)(CorposCelestes[i].getPosY() + CorposCelestes[i].getVelY() * tIteracao + (CorposCelestes[i].getAceleraçãoY() * Math.Pow(tIteracao, 2)) / 2));
            CorposCelestes[i].setVelX((float)(CorposCelestes[i].getVelX() + CorposCelestes[i].getAceleraçãoX() * tIteracao));
            CorposCelestes[i].setVelY((float)(CorposCelestes[i].getVelY() + CorposCelestes[i].getAceleraçãoY() * tIteracao));
        }
 

    }
    public void log(int p)
    {
        Console.WriteLine("----------Interação {0}--------",p);
        for (int i = 0; i < qntCorpos; i++)
        {
            CorposCelestes[i].log();
        }
        

    }

    public void IniciarSimulação()
    {
        for (int i = 0; i < qntIteracoes; i++)
        {
            Simulacao();
            if (i==0)
            {
                Console.WriteLine("{0};{1}",qntCorpos,qntIteracoes);
            }
            else
            {
                log(i);
            }

        }
        
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Corpo primeiro = new Corpo("Primeiro", 350, 5, 1.5f, 2, 0, 0);
        Corpo segundo = new Corpo("Segundo", 75, 5, 1.5f, 10, 0, 0);
        Universo Vialactea = new Universo(2, 10, 50);
        Vialactea.AddCorpo(primeiro);
        Vialactea.AddCorpo(segundo);
        Vialactea.IniciarSimulação();

    }
}