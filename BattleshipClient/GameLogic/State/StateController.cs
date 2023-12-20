using BattleshipClient.GameLogic.State.State_values;

namespace BattleshipClient.GameLogic.State
{
    public class StateController
    {

        GameState defaultState = new DefaultState();
        GameState waitingState = new WaitingState();
        GameState inGameState = new InGameState();
        GameState gameEndedState = new GameEndedState();
        GameState pauseGameState = new PauseState();

        public StateController() 
        {
            defaultState.setNextState(waitingState);
            waitingState.setNextState(inGameState);
            inGameState.setNextState(pauseGameState);
            pauseGameState.setNextState(gameEndedState);
            gameEndedState.setNextState(defaultState);
        }

        public GameState getInitialState()
        {
            return defaultState;
        }

    }
}
