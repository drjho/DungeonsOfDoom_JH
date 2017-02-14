namespace DoD
{
    interface IGameContract
    {
        Room[,] Rooms { get; }
        Player Player { get; }
        void SetInputAndPresenter(IGameInput gameInput, IGamePresenter gamePresenter);
        bool WantToQuit { get; set; }
        Room CurrentRoom { get; }
    }
}