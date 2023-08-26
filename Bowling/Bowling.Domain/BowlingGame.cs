namespace Bowling.Domain;

public class BowlingGame
{
    // Frame
    private bool isFirstThrow = true;
    private int remainingPins = 10;
    
    // History
    private string history = "";

    // Score Keeping
    private int runningStrikes = 0;
    private int runningSpares = 0;

    // Properties
    public string Player { get; }

    public string History { get => history.Trim(); }

    public int Score { get; private set; }

    // Constructors
    public BowlingGame(string player)
    {
        Player = player;
    }

    // Functions
    public void Bowl(int pins)
    {
        // Apply Pins
        Score += pins;
        remainingPins -= pins;

        // Score strikes / spares
        if ((runningStrikes + runningSpares) > 0)
        {
            Score += pins * (runningStrikes + runningSpares);
            runningSpares = runningStrikes;
            runningStrikes = 0;
        }

        // Compute History
        if (remainingPins <= 0)
        {
            if (isFirstThrow)
            {
                history += "X";
                runningStrikes++;
            }
            else
            {
                history += "/";
                runningSpares++;
            }
        }
        else
        {
            history += pins;
        }

        // Manage Frames
        if (isFirstThrow && remainingPins > 0)
        {
            isFirstThrow = false;
        }
        else
        {
            NextFrame();
        }
    }

    private void NextFrame()
    {
        history += " ";
        isFirstThrow = true;
        remainingPins = 10;
    }
}
