namespace Wzl.Src
{
    public interface IWzl
    {
        string[] CreateNewGeneration(string parent);

        string CreateWeasel(int length);

        string AdvanceWeasel(string weasel);

        string GetSoleSurvivorOfNewGeneration(string[] weasels);

        int GetGoalLikenessScore(string weasel);
    }
}
