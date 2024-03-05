using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MMMW_Warcaby
{
    public partial class MainPage : ContentPage
    {
        private readonly List<Piece> Pieces = new List<Piece>();
        private readonly Dictionary<string, List<string>> PossibleCaptures = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> PossibleMoves = new Dictionary<string, List<string>>();
        private string PieceSelected = null;
        private bool IsPlaying = true;
        private char Turn = 'w';
        public MainPage()
        {
            InitializeComponent();
        }

        private void GetPossibleMoves()
        {
            PossibleCaptures.Clear();
            PossibleMoves.Clear();
            List<Piece> visiblePieces = Pieces.Where(p => p.Image.IsVisible).ToList();
            List<Piece> colorPieces = visiblePieces.Where(p => p.Image.ClassId[3] == Turn).ToList();

            foreach (var piece in colorPieces)
            {
                int y = int.Parse(piece.Image.ClassId[1].ToString());
                int x = int.Parse(piece.Image.ClassId[2].ToString());
                string oppositeColor = Turn == 'w' ? "b" : "w";
                List<string> values = new List<string>();
                if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y + 2}{x + 2}") && visiblePieces.Exists(p => p.Image.ClassId == $"p{y + 1}{x + 1}{oppositeColor}") && x <= 5 && y <= 5)
                    values.Add($"i{y + 2}{x + 2}");
                if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y - 2}{x - 2}") && visiblePieces.Exists(p => p.Image.ClassId == $"p{y - 1}{x - 1}{oppositeColor}") && x >= 2 && y >= 2)
                    values.Add($"i{y - 2}{x - 2}");
                if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y - 2}{x + 2}") && visiblePieces.Exists(p => p.Image.ClassId == $"p{y - 1}{x + 1}{oppositeColor}") && x <= 5 && y >= 2)
                    values.Add($"i{y - 2}{x + 2}");
                if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y + 2}{x - 2}") && visiblePieces.Exists(p => p.Image.ClassId == $"p{y + 1}{x - 1}{oppositeColor}") && x >= 2 && y <= 5)
                    values.Add($"i{y + 2}{x - 2}");
                if (values.Count > 0)
                    PossibleCaptures[piece.Image.ClassId] = values;
            }

            if (PossibleCaptures.Count == 0)
            {
                foreach (var piece in colorPieces)
                {
                    int y = int.Parse(piece.Image.ClassId[1].ToString());
                    int x = int.Parse(piece.Image.ClassId[2].ToString());
                    List<string> values = new List<string>();
                    if (piece.Image.ClassId[3] == 'b')
                    {
                        if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y + 1}{x - 1}") && x >= 1 && y <= 6)
                            values.Add($"i{y + 1}{x - 1}");
                        if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y + 1}{x + 1}") && x <= 6 && y <= 6)
                            values.Add($"i{y + 1}{x + 1}");
                    }
                    else
                    {
                        if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y - 1}{x - 1}") && x >= 1 && y >= 1)
                            values.Add($"i{y - 1}{x - 1}");
                        if (!visiblePieces.Exists(p => p.Image.ClassId.Substring(0, 3) == $"p{y - 1}{x + 1}") && x <= 6 && y >= 1)
                            values.Add($"i{y - 1}{x + 1}");
                    }
                    if (values.Count > 0)
                        PossibleMoves[piece.Image.ClassId] = values;
                }
            }
        }

        private void Move(string classId)
        {
            
        }
    }
}
