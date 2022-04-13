// Classe Corpo com os atributos básicos, adicionais e seus devidos métodos
public class Corpo
{   
    // Atributos básicos
    private string Nome;
    private double Massa;
    private double Raio;
    private float PosX;
    private float PosY;
    private float VelX;
    private float VelY;
    // 

    // Atributos adicionais
    private double[,] Força;
    private float[,] Direção;
    private double[,] ForçaXY;
    private double[,] ForçaResultante;
    private double[,] Aceleração;
    //

    public Corpo(string k)  // Construtor de colisão 
    {
        Nome = "Morto";
        Massa = 0.0f;
        Raio = 0.0f;
        PosX = 0;
        PosY = 0;
        VelX = 0;
        VelY = 0;
    }
    public Corpo() // Construtor default 
    {
        Nome = " ";
        Massa = 0.0f;
        Raio = 0.0f;
        PosX = 0;
        PosY = 0;
        VelX = 0;
        VelY = 0;
    }
    public Corpo(string n, double m, double r, float x, float y, float vx, float vy) // Construtor completo
    {
        Nome = n;
        Massa = m;
        Raio = r;
        PosX = x;
        PosY = y;
        VelX = vx;
        VelY = vy;
    }

    //Métodos get/set necessários
    public string getNome()
    {
        return Nome;
    }
    public void setNome(string n)
    {
        Nome = n;
    }
    public double getMassa()
    {
        return Massa;
    }
    public void setMassa(double m)
    {
        Massa = m;
    }
    public void setRaio(double rr)
    {
        Raio = rr;
    }
    public double getRaio()
    {
        return Raio;
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
        PosY = y;
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
    //

    // Inicializador dos atributos adicionais que farão as interações entre os corpos, recebendo a quantidade de corpos para isso
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
    // Metodos get/set dos atributos adicionais
    public void setForçaDir(double f, float x, float y)
    {
        for (int i = 0; i < (Força.Length) / 2; i++)
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
    public double getAngulo(int i, bool t)
    {
        if (t == true)
        {
            return Força[i, 1] * Math.PI / 180; // Retorna em Radiano 
        }
        else
        {
            return Força[i, 1]; // Retorna em Grau
        }

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
        ForçaResultante[0, 0] = fx;
        ForçaResultante[0, 1] = fy;
        ForçaResultante[0, 2] = fr;
    }
    public double getForçaResultanteX()
    {
        return ForçaResultante[0, 0];
    }
    public double getForçaResultanteY()
    {
        return ForçaResultante[0, 1];
    }
    public double getForçaResultanteR()
    {
        return ForçaResultante[0, 2];
    }
    public void setAceleraçãoX(double x)
    {
        Aceleração[0, 0] = x;
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
    //

    //Retorno dos valores das variaveis para mandar para o arquivo de saida
    public string log()
    {
        return string.Format("<{0}>;<{1}>;<{2}>;<{3}>;<{4}>;<{5}>;<{6}>;", Nome, Massa, Raio, PosX, PosY, VelX, VelY);
    }
}

//Classe universo que fará todos os calculos das interações entre um numero n de corpos
public class Universo
{
    private int qntCorpos;
    private int qntIteracoes;
    private float tIteracao;
    Corpo[] CorposCelestes;
    int CorposOcupados = 0; // Variavel para auxiliar no método AddCorpo que contará quantos corpos já foram introduzidos
    public Universo(int c, int q, float t) // Construtor default
    {
        CorposCelestes = new Corpo[c];
        qntIteracoes = q;
        tIteracao = t;
        qntCorpos = c;
    }

    // Métodos de adicionar um corpo ao universo, podendo ser passado como parametro os atibutos ou o objeto pronto
    public void AddCorpo(Corpo k)
    {
        CorposCelestes[CorposOcupados] = k;
        CorposCelestes[CorposOcupados].Interações(qntCorpos - 1);
        CorposOcupados++;
    }
    public void AddCorpo(string n, double m, double r, float x, float y, float vx, float vy)
    {   
        CorposCelestes[CorposOcupados] = new Corpo(n,m,r,x,y,vx,vy);
        CorposCelestes[CorposOcupados].Interações(qntCorpos - 1);
        CorposOcupados++;
    }
    //

    //Método que definitivamente fará os calculos
    public void Simulacao() 
    {
        double f;
        double G = 6.674184 * Math.Pow(10, -11); // Constante de gravitação universal

        // Calculando a força de cada corpo em relação a todos os outros corpos
        for (int i = 0; i < qntCorpos; i++)
        {
            if (CorposCelestes[i].getNome() != "Morto")
            {
                for (int j = i + 1; j < qntCorpos; j++)
                {
                    if (CorposCelestes[j].getNome() != "Morto")
                    {
                        double Distancia = Math.Sqrt(Math.Pow(CorposCelestes[i].getPosX() - CorposCelestes[j].getPosX(), 2.0) + Math.Pow(CorposCelestes[i].getPosY() - CorposCelestes[j].getPosY(), 2.0)); // Formula de distancia entre dois pontos no plano cartesiano
                        if (Distancia < (CorposCelestes[i].getRaio() + CorposCelestes[j].getRaio())) {  // Verificando se a distancia entre os dois corpos não é menor que a soma do raio deles, caso seja ocorreu uma colisão
                            // Inicializando o Corpo destruido.
                            Corpo Destruido = new Corpo("Morto");
                            Destruido.Interações(0);
                            //
                            // Fazendo os calculos para o corpo sobrevivente 
                            if (CorposCelestes[i].getMassa() < CorposCelestes[j].getMassa())
                            {
                                CorposCelestes[j].setMassa(CorposCelestes[i].getMassa() + CorposCelestes[j].getMassa());
                                CorposCelestes[j].setRaio(Math.Sqrt((Math.Pow(CorposCelestes[i].getRaio(), 2.0) + (Math.Pow(CorposCelestes[j].getRaio(), 2.0)))));
                                CorposCelestes[j].setForçaResultante((CorposCelestes[j].getForçaResultanteX() + CorposCelestes[i].getForçaResultanteX()), (CorposCelestes[j].getForçaResultanteY() + CorposCelestes[i].getForçaResultanteY()), (CorposCelestes[j].getForçaResultanteR() + CorposCelestes[i].getForçaResultanteR()));
                                CorposCelestes[j].setVelX(CorposCelestes[j].getVelX() + CorposCelestes[i].getVelX());
                                CorposCelestes[j].setVelY(CorposCelestes[j].getVelY() + CorposCelestes[i].getVelY());
                                CorposCelestes[j].setAceleraçãoX(CorposCelestes[j].getAceleraçãoX() + CorposCelestes[i].getAceleraçãoX());
                                CorposCelestes[j].setAceleraçãoY(CorposCelestes[j].getAceleraçãoY() + CorposCelestes[i].getAceleraçãoY());
                                CorposCelestes[i] = Destruido;
                            }
                            else if (CorposCelestes[i].getMassa() > CorposCelestes[j].getMassa())
                            {
                                CorposCelestes[i].setMassa(CorposCelestes[i].getMassa() + CorposCelestes[j].getMassa());
                                CorposCelestes[i].setRaio(Math.Sqrt((Math.Pow(CorposCelestes[i].getRaio(), 2.0) + (Math.Pow(CorposCelestes[j].getRaio(), 2.0)))));
                                CorposCelestes[i].setForçaResultante((CorposCelestes[j].getForçaResultanteX() + CorposCelestes[i].getForçaResultanteX()), (CorposCelestes[j].getForçaResultanteY() + CorposCelestes[i].getForçaResultanteY()), (CorposCelestes[j].getForçaResultanteR() + CorposCelestes[i].getForçaResultanteR()));
                                CorposCelestes[i].setVelX(CorposCelestes[j].getVelX() + CorposCelestes[i].getVelX());
                                CorposCelestes[i].setVelY(CorposCelestes[j].getVelY() + CorposCelestes[i].getVelY());
                                CorposCelestes[i].setAceleraçãoX(CorposCelestes[j].getAceleraçãoX() + CorposCelestes[i].getAceleraçãoX());
                                CorposCelestes[i].setAceleraçãoY(CorposCelestes[j].getAceleraçãoY() + CorposCelestes[i].getAceleraçãoY());
                                CorposCelestes[j] = Destruido;
                            }
                            else
                            {
                                CorposCelestes[i] = Destruido;
                                CorposCelestes[j] = Destruido;
                            }
                            //
                        }
                        f = G * (CorposCelestes[i].getMassa() * CorposCelestes[j].getMassa()) / Math.Pow(Distancia, 2); // Lei da Gravitação Universal 
                        CorposCelestes[i].setForçaDir(f, CorposCelestes[j].getPosX(), CorposCelestes[j].getPosY()); // Apontando para o ponto (x,y) em que está o outro corpo
                        CorposCelestes[j].setForçaDir(f, CorposCelestes[i].getPosX(), CorposCelestes[i].getPosY()); // Apontando para o ponto (x,y) em que está o outro corpo
                    }
                }
            }
        }
        for (int i = 0; i < qntCorpos; i++)
        {
            if (CorposCelestes[i].getNome() != "Morto")
            {
                for (int j = 0; j < qntCorpos - 1; j++)
                {
                    if (CorposCelestes[i].getNome() != "Morto")
                    {
                        // Angulo de cada Força para poder decompor em X e Y
                        double AjustaAngulo = 0;

                        // Análisando em qual quadrante está o vetor da força resultante
                        
                        // Primeiro Quadrante
                        if ((CorposCelestes[i].getDirY(j) > CorposCelestes[i].getPosY()) & (CorposCelestes[i].getDirX(j) > CorposCelestes[i].getPosX()))
                        {
                            AjustaAngulo = (Math.Atan2((CorposCelestes[i].getDirY(j) - CorposCelestes[i].getPosY()), (CorposCelestes[i].getDirX(j) - CorposCelestes[i].getPosX())));
                            AjustaAngulo = AjustaAngulo * 180 / (Math.PI);
                            CorposCelestes[i].setAngulo(j, AjustaAngulo);
                        }
                        // Segundo Quadrante
                        else if ((CorposCelestes[i].getDirY(j) > CorposCelestes[i].getPosY()) & (CorposCelestes[i].getDirX(j) < CorposCelestes[i].getPosX()))
                        {
                            AjustaAngulo = Math.Atan2((CorposCelestes[i].getDirY(j) - CorposCelestes[i].getPosY()), (CorposCelestes[i].getPosX() - CorposCelestes[i].getDirX(j)));
                            AjustaAngulo = AjustaAngulo * 180 / (Math.PI);
                            AjustaAngulo = 180 - AjustaAngulo;
                            CorposCelestes[i].setAngulo(j, AjustaAngulo);
                        }
                        // Terceiro Quadrante
                        else if ((CorposCelestes[i].getDirY(j) < CorposCelestes[i].getPosY()) & (CorposCelestes[i].getDirX(j) < CorposCelestes[i].getPosX()))
                        {
                            AjustaAngulo = Math.Atan2((CorposCelestes[i].getPosY() - CorposCelestes[i].getDirY(j)), (CorposCelestes[i].getPosX() - CorposCelestes[i].getDirX(j)));
                            AjustaAngulo = AjustaAngulo * 180 / (Math.PI);
                            AjustaAngulo = 180 + AjustaAngulo;
                            CorposCelestes[i].setAngulo(j, AjustaAngulo);
                        }
                        // Quarto Quadrante
                        else if ((CorposCelestes[i].getDirY(j) < CorposCelestes[i].getPosY()) & (CorposCelestes[i].getDirX(j) > CorposCelestes[i].getPosX()))
                        {
                            AjustaAngulo = Math.Atan2((CorposCelestes[i].getPosY() - CorposCelestes[i].getDirY(j)), (CorposCelestes[i].getDirX(j) - CorposCelestes[i].getPosX()));
                            AjustaAngulo = AjustaAngulo * 180 / (Math.PI);
                            AjustaAngulo = 360 - AjustaAngulo;
                            CorposCelestes[i].setAngulo(j, AjustaAngulo);
                        }
                        //Sob algum eixo igual
                        else
                        {   // 1800 = Sob o eixo x  e 1801 = Sob o eixo y
                            if (((CorposCelestes[i].getDirY(j) == CorposCelestes[i].getPosY()) & CorposCelestes[i].getDirX(j) != CorposCelestes[i].getPosX()))
                            {
                                CorposCelestes[i].setAngulo(j, 1800);
                            }
                            else if ((CorposCelestes[i].getDirY(j) != CorposCelestes[i].getPosY()) & CorposCelestes[i].getDirX(j) == CorposCelestes[i].getPosX())
                            {
                                CorposCelestes[i].setAngulo(j, 1801);
                            }

                        }
                        //
                    }
                }
            }
        }

        // Decompondo a força em XY
        for (int i = 0; i < qntCorpos; i++)
        {
            if (CorposCelestes[i].getNome() != "Morto")
            {
                for (int j = 0; j < qntCorpos - 1; j++)
                {
                    if (CorposCelestes[i].getNome() != "Morto")
                    {
                        if (CorposCelestes[i].getAngulo(j, false) == 1800) // Sob o eixo X
                        {
                            CorposCelestes[i].setForçaX(j, CorposCelestes[i].getForça(j)); // Se estão sob o eixo X a força em x é igual a força resultante
                            if (CorposCelestes[i].getDirX(j) < CorposCelestes[i].getPosX())
                            {
                                CorposCelestes[i].setForçaX(j, CorposCelestes[i].getForçaX(j) * -1); // Colocando os devidos sinais adotanto X para esquerda como negativo
                            }
                            CorposCelestes[i].setForçaY(j, 0);
                        }
                        else if (CorposCelestes[i].getAngulo(j, false) == 1801) // Sob o eixo Y
                        {
                            CorposCelestes[i].setForçaY(j, CorposCelestes[i].getForça(j)); // Se estão sob o eixo Y a força em y é igual a força resultante
                            if (CorposCelestes[i].getDirY(j) < CorposCelestes[i].getPosY())
                            {
                                CorposCelestes[i].setForçaY(j, CorposCelestes[i].getForçaY(j) * -1); // Colocando os devidos sinais adotanto Y para baixo como negativo
                            }
                            CorposCelestes[i].setForçaX(j, 0);
                        }
                        else
                        {
                            // Decompondo a força em xy multiplicando a força resultante pelo angulo com o eixo x
                            CorposCelestes[i].setForçaX(j, CorposCelestes[i].getForça(j) * Math.Cos(CorposCelestes[i].getAngulo(j, true)));
                            CorposCelestes[i].setForçaY(j, CorposCelestes[i].getForça(j) * Math.Sin(CorposCelestes[i].getAngulo(j, true)));
                            //
                        }
                    }
                }
            }
        }
        //Soma das forças X,Y,calculo da força resultante entre elas e das acelerações consequentes
        for (int i = 0; i < qntCorpos; i++)
        {
            if (CorposCelestes[i].getNome() != "Morto")
            {
                double fx = 0;
                double fy = 0;
                double fr = 0;
                for (int j = 0; j < qntCorpos - 1; j++) // Soma das forças XY
                {
                    fx += CorposCelestes[i].getForçaX(j);
                    fy += CorposCelestes[i].getForçaY(j);
                }
                fr = Math.Sqrt(Math.Pow(fx, 2) + Math.Pow(fy, 2)); // Pitagoras sendo a força resultante a hipotenusa
                CorposCelestes[i].setForçaResultante(fx, fy, fr);
                // Descobrindo a aceleração em XY usando f=ma
                CorposCelestes[i].setAceleraçãoX(fx / CorposCelestes[i].getMassa());
                CorposCelestes[i].setAceleraçãoY(fy / CorposCelestes[i].getMassa());
                //
            }
        }
        // Formula de posição e velocidade 
        for (int i = 0; i < qntCorpos; i++)
        {
            if (CorposCelestes[i].getNome() != "Morto")
            {
                CorposCelestes[i].setPosX((float)(CorposCelestes[i].getPosX() + CorposCelestes[i].getVelX() * tIteracao + (CorposCelestes[i].getAceleraçãoX() * Math.Pow(tIteracao, 2)) / 2));
                CorposCelestes[i].setPosY((float)(CorposCelestes[i].getPosY() + CorposCelestes[i].getVelY() * tIteracao + (CorposCelestes[i].getAceleraçãoY() * Math.Pow(tIteracao, 2)) / 2));
                CorposCelestes[i].setVelX((float)(CorposCelestes[i].getVelX() + CorposCelestes[i].getAceleraçãoX() * tIteracao));
                CorposCelestes[i].setVelY((float)(CorposCelestes[i].getVelY() + CorposCelestes[i].getAceleraçãoY() * tIteracao));
            }
        }
    }

    // Método que fará a repetição da simulação de acordo com o valor presente no arquivo de entrada e escreverá no arquivo de saida
    public void IniciarSimulação() 
    {

        StreamWriter escrita = new StreamWriter("Saida.txt");
        escrita.WriteLine("{0};{1}", qntCorpos, qntIteracoes);

        for (int i = 0; i < qntIteracoes; i++)
        {
            escrita.WriteLine("----------Interação{0}--------", i);
            for (int j = 0; j < qntCorpos; j++)
            {
                escrita.WriteLine(CorposCelestes[j].log());
            }
            Simulacao();
        }
        escrita.Close();
    }
}

class Program
{
    public static void Main(string[] args)
    {   
        char[] delimitadores = { ';', '\n', '<', '>','\r' };
        StreamReader leitura = new StreamReader("Entrada.txt");
        string leitor = leitura.ReadToEnd(); // Lendo o arquivo inteiro
        string[] LeituraCompleta = leitor.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries); // Quebrando ele de acordo com os delimitadores
        leitura.Close();
        Universo Vialactea = new Universo(int.Parse(LeituraCompleta[0]),int.Parse(LeituraCompleta[1]),int.Parse(LeituraCompleta[2])); // Criando o universo com os dados da primeira linha
        int aux = 3;
        for (int i = 0; i < (int.Parse(LeituraCompleta[0])); i++)
        {
            Vialactea.AddCorpo(LeituraCompleta[aux], double.Parse(LeituraCompleta[aux+1]), double.Parse(LeituraCompleta[aux + 2]), float.Parse(LeituraCompleta[aux + 3]), float.Parse(LeituraCompleta[aux + 4]), float.Parse(LeituraCompleta[aux + 5]), float.Parse(LeituraCompleta[aux+6])); // Adicionando o corpo presente em cada linha ao universo criado
            aux += 7;        
        }
        Vialactea.IniciarSimulação(); // Simula e escreve no arquivo de saida
    }
}