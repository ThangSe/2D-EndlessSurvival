using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManaSystem
{
    public event EventHandler OnManaChanged;

    private int mana;
    private int manaMax;

    public ManaSystem(int manaMax)
    {
        this.manaMax = manaMax;
        mana = manaMax;
    }

    public int GetMana()
    {
        return mana;
    }

    public float GetManaPercent()
    {
        return (float)mana / manaMax;
    }

    public bool UsingMana(int manaCost)
    {
        if (mana < manaCost)
        {
            return false;
        } else
        {
            mana -= manaCost;
            OnManaChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        
    }

    public void UsingPotion(int manaGainAmount)
    {
        mana += manaGainAmount;
        if (mana > manaMax) mana = manaMax;
        OnManaChanged?.Invoke(this, EventArgs.Empty);
    }

    
    public void ManaRegen()
    {
        mana++;
        if (mana > manaMax) mana = manaMax;
        OnManaChanged?.Invoke(this, EventArgs.Empty);
    }
}
