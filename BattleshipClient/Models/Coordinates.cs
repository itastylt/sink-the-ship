namespace BattleshipClient.Models
{
    public class Coordinates
    {
        int x { get; set; }
        int y { get; set; }
        public Coordinates(int y, int x)
        {
            this.x = x;
            this.y = y;
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
    }
}
