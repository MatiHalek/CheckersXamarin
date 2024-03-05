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
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }

        private void AutoMove_Clicked(object sender, EventArgs e)
        {

        }
    }
}
