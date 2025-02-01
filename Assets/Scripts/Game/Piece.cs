namespace Game
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Bishop,
        Knight,
        Queen,
        King,
        Empty
    }

    public enum Color
    {
        Black,
        White,
        None
    }
    public class Piece
    {
        public PieceType pieceType;
        public Color pieceColor;

        public Piece(PieceType pieceType, Color pieceColor)
        {
            this.pieceType = pieceType;
            this.pieceColor = pieceColor;
        }
    }
}