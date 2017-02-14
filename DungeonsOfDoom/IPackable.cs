namespace DoD
{
    internal interface IPackable
    {
        void UseOrDrop(Player player, Room room);
        void EnhancePlayer(Player player);
        void LowerPlayer(Player player);
        string GetAttack();
        string GetRegeneration();
        string GetDefense();

        int Weight { get; set; }
        string Name { get;}
        string Description { get; }
        int Id { get; set; }
    }
}