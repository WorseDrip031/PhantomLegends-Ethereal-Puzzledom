using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float Attack;
    [SerializeField] float Defense;
    [SerializeField] float Speed;
    [SerializeField] float XP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getPlayerAttack()
    {
        return Attack;
    }

    public float getPlayerSpeed()
    {
        return Speed;
    }

    public float getPlayerXP()
    {
        return XP;
    }

    public void increaseXP(float amount)
    {
        XP += amount;
    }
}
