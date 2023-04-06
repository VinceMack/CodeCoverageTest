using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value
{
    public enum Condition { equal, notEqual, greaterThan, lessThan }

    private Condition myCondition;
    private int threshold;

    public Value(int threshold, Condition condition)
    {
        this.threshold = threshold;
        this.myCondition = condition;
    }
    
    public bool ConditionMet(int value)
    {
        switch (myCondition)
        {
            case Condition.equal:
            {
                return value == threshold;
            }
            case Condition.notEqual:
            {
                return value != threshold;
            }
            case Condition.greaterThan:
            {
                return value > threshold;
            }
            case Condition.lessThan:
            {
                return value < threshold;
            }
        }
        return false;
    }
}
