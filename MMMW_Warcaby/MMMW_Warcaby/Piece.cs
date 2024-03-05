using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MMMW_Warcaby
{
    public class Piece
    {
        public ImageButton Image = new ImageButton();
        public bool IsEnabled { get; set; } = true;
        public Piece(string image, bool isWhite, int positionY, int positionX)
        {
            Image.Source = ImageSource.FromResource(image);
            Image.ClassId = "p" + positionY + positionX + (isWhite ? "w" : "b");
            Grid.SetRow(Image, positionY);
            Grid.SetColumn(Image, positionX);
        }
    }
}
