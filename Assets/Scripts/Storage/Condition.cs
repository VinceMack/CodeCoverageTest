using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public enum Operation { equal, notEqual, greaterThan, lessThan }

    private Operation myOperation;
    private int threshold;

    public Condition(int threshold, Operation operation)
    {
        this.threshold = threshold;
        this.myOperation = operation;
    }
    
    public bool ConditionMet(int value)
    {
        switch (myOperation)
        {
            case Operation.equal:
            {
                return value == threshold;
            }
            case Operation.notEqual:
            {
                return value != threshold;
            }
            case Operation.greaterThan:
            {
                return value > threshold;
            }
            case Operation.lessThan:
            {
                return value < threshold;
            }
        }
        return false;
    }
}
