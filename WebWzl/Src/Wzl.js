/*

// Implement the interface

string[] CreateNewGeneration(string parent);

string CreateWeasel(int length);

string Advanceweasel(string weasel);

string GetSoleSurvivorOfNewGeneration(string[] weasels);

int GetGoalLikenessScore(string weasel);

*/

function Wzl(numPerGeneration, mutationChancePercent, set, goal) {

    this.NumPerGeneration = numPerGeneration;

    this.MutationChancePercent = mutationChancePercent;

    this.Set = set;

    this.Goal = goal;

}

// Taken from MDN manual
Wzl.prototype.GetRandomIntInclusive = function (min, max) {

    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;

};

Wzl.prototype.ReplaceChar = function (s, index, newChar) {

    return s.substr(0, index) + newChar + s.substr(index + 1);

};

Wzl.prototype.CreateNewGeneration = function (parent) {

    var children = [];

    for(var i = 0; i < this.NumPerGeneration; i++)
    {
        children.push(parent);
    }

    return children;

};

Wzl.prototype.CreateWeasel = function (length) {

    var weasel = "";

    for(var i = 0; i < length; i++)
    {
        weasel += this.Set.charAt(this.GetRandomIntInclusive(0, this.Set.length - 1));
    }

    return weasel;

};

Wzl.prototype.AdvanceWeasel = function (weasel) {

    for(var i = 0; i < weasel.length; i++)
    {
        var randNum = this.GetRandomIntInclusive(0, 99);

        if(randNum < this.MutationChancePercent)
        {
            weasel = this.ReplaceChar(weasel, i, this.Set.charAt(this.GetRandomIntInclusive(0, this.Set.length)));
        }
    }

    return weasel;

};

Wzl.prototype.GetSoleSurvivorOfNewGeneration = function (weasels) {

    var bestWeasel = weasels[0];
    var bestScore = 0;

    for(var i = 0; i < this.NumPerGeneration; i++)
    {
        var score = this.GetGoalLikenessScore(weasels[i]);

        if(score > bestScore)
        {
            bestScore = score;
            bestWeasel = weasels[i];
        }
    }

    return bestWeasel;

};

Wzl.prototype.GetGoalLikenessScore = function (weasel) {

    var score = 0;

    for (var i = 0; i < weasel.length; i++)
    {
        if (weasel.charAt(i) == this.Goal.charAt(i))
            score++;
    }

    return score;

};