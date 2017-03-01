using System.IO;

namespace TP1
{
    class Config
    {

        private static Config instance;

        public static Config configInstance => instance ?? (instance = new Config()); //singleton

        public string CheminEntree { get; }
        public string CheminSortie { get; }
        public string FenetreTaille { get; }
        public string CodeCorrecteur { get; }
        public string TypeDeRejet { get; }
        public string Erreurs { get; }

        private Config()
        {
            StreamReader sr = File.OpenText("../../config.txt");
            CheminEntree = sr.ReadLine();
            CheminEntree = CheminEntree?.Substring(CheminEntree.IndexOf(':') + 2) ?? string.Empty;

            CheminSortie = sr.ReadLine();
            CheminSortie = CheminSortie?.Substring(CheminSortie.IndexOf(':') + 2) ?? string.Empty;

            FenetreTaille = sr.ReadLine();
            FenetreTaille = FenetreTaille?.Substring(FenetreTaille.IndexOf(':') + 2) ?? string.Empty;

            CodeCorrecteur = sr.ReadLine();
            CodeCorrecteur = CodeCorrecteur?.Substring(CodeCorrecteur.IndexOf(':') + 2) ?? string.Empty;

            TypeDeRejet = sr.ReadLine();
            TypeDeRejet = TypeDeRejet?.Substring(TypeDeRejet.IndexOf(':') + 2) ?? string.Empty;

            Erreurs = sr.ReadLine();
            Erreurs = Erreurs?.Substring(Erreurs.IndexOf(':') + 2) ?? string.Empty;
        }

    }
}
