namespace DoD
{
    internal interface IMovable
    {
        void Move(Player player, Room[,] rooms);
        int X { get; set; }
        int Y { get; set; }
    }
}