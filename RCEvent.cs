using System;
using System.Collections.Generic;

public class RCEvent
{
    private RCCondition condition;
    private RCAction elseAction;
    private int eventClass;
    private int eventType;
    public string foreachVariableName;
    public List<RCAction> trueActions;

    public RCEvent(RCCondition sentCondition, List<RCAction> sentTrueActions, int sentClass, int sentType)
    {
        this.condition = sentCondition;
        this.trueActions = sentTrueActions;
        this.eventClass = sentClass;
        this.eventType = sentType;
    }

    public void checkEvent()
    {
        int num;
        switch (this.eventClass)
        {
            case 0:
                num = 0;
                while (num < this.trueActions.Count)
                {
                    this.trueActions[num].doAction();
                    num++;
                }
                return;

            case 1:
                if (this.condition.checkCondition())
                {
                    for (num = 0; num < this.trueActions.Count; num++)
                    {
                        this.trueActions[num].doAction();
                    }
                    return;
                }
                if (this.elseAction == null)
                {
                    break;
                }
                this.elseAction.doAction();
                return;

            case 2:
                switch (this.eventType)
                {
                    case 0:
                        foreach (TITAN titan in FengGameManagerMKII.instance.titans)
                        {
                            if (FengGameManagerMKII.titanVariables.ContainsKey(this.foreachVariableName))
                            {
                                FengGameManagerMKII.titanVariables[this.foreachVariableName] = titan;
                            }
                            else
                            {
                                FengGameManagerMKII.titanVariables.Add(this.foreachVariableName, titan);
                            }
                            foreach (RCAction action in this.trueActions)
                            {
                                action.doAction();
                            }
                        }
                        return;

                    case 1:
                        foreach (PhotonPlayer player in PhotonNetwork.playerList)
                        {
                            if (FengGameManagerMKII.playerVariables.ContainsKey(this.foreachVariableName))
                            {
                                FengGameManagerMKII.playerVariables[this.foreachVariableName] = player;
                            }
                            else
                            {
                                FengGameManagerMKII.titanVariables.Add(this.foreachVariableName, player);
                            }
                            foreach (RCAction action2 in this.trueActions)
                            {
                                action2.doAction();
                            }
                        }
                        return;
                }
                return;

            case 3:
                while (this.condition.checkCondition())
                {
                    foreach (RCAction action3 in this.trueActions)
                    {
                        action3.doAction();
                    }
                }
                break;

            default:
                return;
        }
    }

    public void setElse(RCAction sentElse)
    {
        this.elseAction = sentElse;
    }

    public enum foreachType
    {
        titan,
        player
    }

    public enum loopType
    {
        noLoop,
        ifLoop,
        foreachLoop,
        whileLoop
    }
}

