using System.IO;

namespace TP1
{
    class Config
    {

        private static Config instance;

        public static Config ConfigInstance => instance ?? (instance = new Config()); //singleton

        public string CheminEntree { get; }
        public string CheminSortie { get; }
        public string FenetreTailleEmetteur { get; }
        public string FenetreTailleRecepteur { get; }
        private string CodeCorrecteur_;
        public bool CodeCorrecteur { get { return CodeCorrecteur_ == "true"; } }
        public string TypeDeRejet { get; }
        public string Erreurs { get; }

        private Config()
        {
            StreamReader sr = File.OpenText("../../config.txt");
            CheminEntree = sr.ReadLine();
            CheminEntree = CheminEntree?.Substring(CheminEntree.IndexOf(':') + 2) ?? string.Empty;

            CheminSortie = sr.ReadLine();
            CheminSortie = CheminSortie?.Substring(CheminSortie.IndexOf(':') + 2) ?? string.Empty;

            FenetreTailleEmetteur = sr.ReadLine();
            FenetreTailleEmetteur = FenetreTailleEmetteur?.Substring(FenetreTailleEmetteur.IndexOf(':') + 2) ?? string.Empty;

            FenetreTailleRecepteur = sr.ReadLine();
            FenetreTailleRecepteur = FenetreTailleRecepteur?.Substring(FenetreTailleRecepteur.IndexOf(':') + 2) ?? string.Empty;

            CodeCorrecteur_ = sr.ReadLine();
            CodeCorrecteur_ = CodeCorrecteur_?.Substring(CodeCorrecteur_.IndexOf(':') + 2) ?? string.Empty;

            TypeDeRejet = sr.ReadLine();
            TypeDeRejet = TypeDeRejet?.Substring(TypeDeRejet.IndexOf(':') + 2) ?? string.Empty;

            Erreurs = sr.ReadLine();
            Erreurs = Erreurs?.Substring(Erreurs.IndexOf(':') + 2) ?? string.Empty;

            sr.Close();
            sr.Dispose();
        }

    }
}
