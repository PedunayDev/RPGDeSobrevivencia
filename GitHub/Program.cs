
class Program {
    static void Main() {
        RPGDeSobrevivencia();
    }

    // RPG DE SOBREVIVENCIA
    class Jogador {
        int vida = 100;
        int moeda = 0;
        int guardar = 0;

        public int Moeda {
            get { return moeda; }
            set { moeda = value; }
        }

        public int Vida {
            get { return vida; }
            set {
                vida = value;
                if (value < 0) vida = 0;
                if (value > 100) vida = 100;
            }
        }

        public void TomarDano(int dano) {
            Vida -= dano;
        }

        public void Curar(int cura) {
            Vida += cura;
        }

        public void GanharMoedas(int moeda) {
            Moeda += moeda;
        }

        public void PerderMoedas(int moeda) {
            if (Moeda - moeda >= 0)
                Moeda -= moeda;
        }

        // CAVERNA
        public void ExplorarCaverna(Jogador jg1, Random rng) {
            int evento = rng.Next(1, 4);

            if (evento == 1) {
                guardar = rng.Next(10, 51);
                jg1.TomarDano(guardar);
                Console.WriteLine($"Voce encontrou um monstro e perdeu {guardar} pontos de vida.");
            }

            if (evento == 2) {
                guardar = rng.Next(5, 21);
                jg1.GanharMoedas(guardar);
                Console.WriteLine($"Voce encontrou um tesouro e ganhou {guardar} moedas.");
            }

            if (evento == 3) {
                guardar = rng.Next(10, 26);
                jg1.Curar(guardar);
                Console.WriteLine($"Voce encontrou uma pocao e recuperou ate {guardar} pontos de vida.");
            }
        }

        // LOJA
        public void Loja(Jogador jg1, Random rng) {
            if (jg1.Moeda < 10) {
                Console.WriteLine("Voce precisa de pelo menos 10 moedas para comprar um item.");
            }
            else {
                Console.WriteLine($"LOJA: (Voce tem {jg1.Moeda} moedas)");
                Console.WriteLine("1- Pocao simples que cura 10 de vida, custa 10 moedas");
                Console.WriteLine("2- Pocao media que cura 20 de vida, custa 20 moedas");
                Console.WriteLine("3- Pocao ultra que cura 30 de vida, custa 30 moedas");
                Console.WriteLine("4- Pocao misteriosa da bruxa. Custa 15 moedas");
                Console.WriteLine(" ");
                Console.WriteLine("Escolha uma opcao:");

                int.TryParse(Console.ReadLine(), out int numero);

                if (numero == 1 && jg1.Moeda >= 10) {
                    jg1.Curar(10);
                    jg1.PerderMoedas(10);
                    Console.WriteLine("Voce comprou uma pocao simples, recuperou ate 10 pontos de vida e gastou 10 moedas.");
                }
                else if (numero == 2 && jg1.Moeda >= 20) {
                    jg1.Curar(20);
                    jg1.PerderMoedas(20);
                    Console.WriteLine("Voce comprou uma pocao media, recuperou ate 20 pontos de vida e gastou 20 moedas.");
                }
                else if (numero == 3 && jg1.Moeda >= 30) {
                    jg1.Curar(30);
                    jg1.PerderMoedas(30);
                    Console.WriteLine("Voce comprou uma pocao ultra, recuperou ate 30 pontos de vida e gastou 30 moedas.");
                }
                else if (numero == 4 && jg1.Moeda >= 15) {
                    jg1.PerderMoedas(15);
                    int bruxa = rng.Next(1, 5);

                    if (bruxa == 1 || bruxa == 2) {
                        guardar = rng.Next(30, 70);
                        int vidaAnterior = jg1.Vida;
                        jg1.Curar(guardar);
                        Console.WriteLine($"A pocao teve um efeito positivo! Voce recuperou {jg1.Vida - vidaAnterior} pontos de vida.");
                    }
                    else if (bruxa == 3) {
                        guardar = rng.Next(30, 70);
                        jg1.TomarDano(guardar);
                        Console.WriteLine($"A pocao teve um efeito negativo! Voce perdeu {guardar} pontos de vida.");
                    }
                    else {
                        jg1.TomarDano(100);
                        Console.WriteLine("A pocao teve um efeito muito negativo! Voce perdeu 100 pontos de vida.");
                    }
                }
                else {
                    jg1.TomarDano(5);
                    Console.WriteLine("Opcao invalida ou saldo insuficiente. Voce perdeu 5 pontos de vida.");
                }
            }
        }

        // DESCANSAR
        public void Descansar(Jogador jg1, Random rng) {
            int evento2 = rng.Next(1, 3);

            if (evento2 == 2) {
                jg1.Curar(15);
                Console.WriteLine("Voce descansou sem ser atacado e recuperou ate 15 pontos de vida.");
            }
            else {
                jg1.TomarDano(15);
                Console.WriteLine("Voce foi atacado durante o descanso e perdeu 15 pontos de vida.");
            }
        }
    }

    static void RPGDeSobrevivencia() {
        Jogador jg1 = new Jogador();
        int rounds = 1;
        bool bloquearRound;
        bool jogo = true;
        Random rng = new Random();

        while (jogo) {
            bloquearRound = false;

            if (rounds == 1) {
                Console.WriteLine("RPG DE SOBREVIVENCIA");
            }

            Console.WriteLine(" ");
            Console.WriteLine($"VIDA: {jg1.Vida}");
            Console.WriteLine($"MOEDAS: {jg1.Moeda}");
            Console.WriteLine($"ROUND: {rounds}");

            Console.WriteLine(" ");
            Console.WriteLine("Escolha uma opcao:");
            Console.WriteLine("1- Explorar a caverna");
            Console.WriteLine("2- Loja");
            Console.WriteLine("3- Descansar");

            int.TryParse(Console.ReadLine(), out int valor);

            if (valor == 1) {
                jg1.ExplorarCaverna(jg1, rng);
            }
            else if (valor == 2) {
                jg1.Loja(jg1, rng);
            }
            else if (valor == 3) {
                jg1.Descansar(jg1, rng);
            }
            else {
                jg1.TomarDano(5);
                bloquearRound = true;
                Console.WriteLine("Opcao invalida. Voce perdeu 5 pontos de vida.");
            }

            if (jg1.Vida > 0 && bloquearRound == false) {
                rounds++;
            }

            if (jg1.Vida <= 0) {
                jogo = false;
            }
        }

        Console.WriteLine("FIM DE JOGO!");
        Console.WriteLine($"Rounds: {rounds}");
        Console.WriteLine($"Moedas: {jg1.Moeda}");
    }
} 
