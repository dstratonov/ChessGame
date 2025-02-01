namespace Game
{
    public class Cell
    {
        public int HeightIndex;
        public int WidthIndex;

        public Piece piece;

        public Cell(int x, int y)
        {
            HeightIndex = y;
            WidthIndex = x;
        }

        public void addPiece(Piece p)
        {
            piece = p;
        }
    }
}