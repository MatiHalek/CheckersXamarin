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

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
