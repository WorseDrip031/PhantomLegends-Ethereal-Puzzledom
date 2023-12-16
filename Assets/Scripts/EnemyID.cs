using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyID
{
    private static int nextID = 0;

    public static int GetEnemyID()
    {
        nextID += 1;
        return nextID;
    }
}
