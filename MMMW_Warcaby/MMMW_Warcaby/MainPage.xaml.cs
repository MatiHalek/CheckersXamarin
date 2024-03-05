using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
            bool isWhite = true;
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8 ; j++)
                {
                    Button button = new Button();
                    button.Clicked += Button_Clicked;
                    if (isWhite)
                        button.BackgroundColor = Color.White;
                    else
                        button.BackgroundColor = Color.FromRgb(160, 82, 46);
                    button.ClassId = "i" + i + j;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    grid.Children.Add(button);
                    isWhite = !isWhite;
                }
                isWhite = !isWhite;
            }
            for(int i = 0; i < 3; i ++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(i % 2 == 0 && j % 2 == 1 || i % 2 == 1 && j % 2 == 0)
                        Pieces.Add(new Piece("MMMW_Warcaby.Images.black.png", false, i, j));
                    else
                        Pieces.Add(new Piece("MMMW_Warcaby.Images.white.png", true, 7 - i, j));
                    Pieces.Last().Image.BackgroundColor = Color.Transparent;
                    Pieces.Last().Image.Clicked += Image_Clicked;
                    grid.Children.Add(Pieces.Last().Image); 
                }
            }
            call function
        }

        private void Image_Clicked(object sender, EventArgs e)
        {
            var image = (ImageButton)sender;
            if (!IsPlaying || image.ClassId[3] != Turn)
                return;
            if(PieceSelected is null)
            {
                PieceSelected = image.ClassId;
                image.BackgroundColor = Color.FromHex("#B3228B22");
            }
            else if(PieceSelected == image.ClassId)
            {
                PieceSelected = null;
                image.BackgroundColor = Color.Transparent;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (PieceSelected != null)
                Move(((Button)sender).ClassId);
        }

        private void AutoMove_Clicked(object sender, EventArgs e)
        {
            if(IsPlaying)
            {
                Random random = new Random();
                var allMoves = PossibleCaptures.Concat(PossibleMoves).ToDictionary(item => item.Key, item => item.Value);
                int index = -1;
                if (PieceSelected != null)
                    index = allMoves.Keys.IndexOf(PieceSelected);
                else
                {
                    index = random.Next(allMoves.Count);
                    PieceSelected = allMoves.Keys.ElementAt(index);
                }
                if(index != -1)
                {
                    var piece = allMoves.ElementAt(index);
                    var move = piece.Value[random.Next(piece.Value.Count)]; 
                    Move(move);
                }
            }
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
            var piece = Pieces.Find(p => p.Image.IsVisible && p.Image.ClassId == PieceSelected);
            if (PossibleCaptures.ContainsKey(PieceSelected) && PossibleCaptures[PieceSelected].Contains(classId) || PossibleMoves.ContainsKey(PieceSelected) && PossibleMoves[PieceSelected].Contains(classId))
            {
                piece.Image.TranslateTo(piece.Image.TranslationX + (classId[2] - PieceSelected[2]) * 40, piece.Image.TranslationY + (classId[1] - PieceSelected[1]) * 40, 300);
                piece.Image.ClassId = "p" + classId[1] + classId[2] + Turn;
                piece.Image.BackgroundColor = Color.Transparent;
                if (PossibleCaptures.ContainsKey(PieceSelected) && PossibleCaptures[PieceSelected].Contains(classId))
                {
                    Pieces.Find(p => p.Image.IsVisible && p.Image.ClassId == "p" + ((int.Parse(classId[1].ToString()) + int.Parse(PieceSelected[1].ToString())) / 2) + ((int.Parse(classId[2].ToString()) + int.Parse(PieceSelected[2].ToString())) / 2) + (Turn == 'w' ? "b" : "w")).Image.IsVisible = false;
                    PieceSelected = null;
                    GetPossibleMoves();
                    if (PossibleCaptures.Count > 0)
                        return;
                    if (Pieces.Where(p => p.Image.ClassId[3] != Turn).All(p => !p.Image.IsVisible))
                    {
                        IsPlaying = false;
                        DisplayAlert("Koniec gry", $"{(Turn == 'w' ? "Białe" : "Czarne")} wygrały", "OK");
                    }
                }
                PieceSelected = null;
                Turn = Turn == 'w' ? 'b' : 'w';
                GetPossibleMoves();
                if (PossibleCaptures.Count == 0 && PossibleMoves.Count == 0)
                {
                    IsPlaying = false;
                    DisplayAlert("Koniec gry", $"Remis - brak możliwości ruchu", "OK");
                }
            }
        }
    }
}
