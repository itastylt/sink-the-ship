using BattleshipClient.GameLogic.State.State_values;

namespace BattleshipClient.GameLogic.State
{
    public class StateController
    {

        GameState waitingState = new WaitingState();
        GameState inGameState = new InGameState();
        GameState pauseGameState = new PauseState();
        GameState waitingToUnpause = new UnpauseWaitingState();

        public StateController()
        {
            inGameState.setNextState(waitingState);
            waitingState.setNextState(pauseGameState);
            pauseGameState.setNextState(waitingToUnpause);
            waitingToUnpause.setNextState(inGameState);
        }

        public GameState getInitialState()
        {
            return inGameState;
        }

    }
}
