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

        }
    }
}
