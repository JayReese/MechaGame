using System.Collections;

public enum SpiderMechState { Spawning, Traveling, Armed, Detonated }

public class Spider
{
    private SpiderMechState spiderstate;
    public SpiderMechState spiderState
    {
        get { return spiderstate; }
        private set { spiderstate = value; }
    }

    public Spider()
    {
        spiderstate = SpiderMechState.Spawning;
    }

    public void ChangeStateExt(SpiderMechState spState)
    {
        spiderstate = spState;
    }

}
