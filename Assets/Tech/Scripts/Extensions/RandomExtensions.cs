using UnityEngine;

public static class RandomExtensions
{
    public static bool RandomBool(this bool b, int prob = 5) 
    {
        if (prob > 10) prob = 10;
        else if (prob < 0) prob = 0;        
        return Random.Range(0, 10) < prob; 
    }
}