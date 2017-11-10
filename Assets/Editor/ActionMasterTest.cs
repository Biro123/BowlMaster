using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionMasterTest {

    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;


    [SetUp]
    public void Setup ()  // runs at the beginning of *each* test
    {
        pinFalls = new List<int>();
    }

    [Test]
    public void T00_PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls) );
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03_SecondBowlReturnsEndTurn()
    {
        int[] rolls = { 3, 1 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T04_SpareReturnsEndTurn()
    {
        int[] rolls = { 3, 7 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T05_Bowl0()
    {
        int[] rolls = { 0 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T06_Bowl0then10()
    {
        int[] rolls = { 0,10 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T07_Bowl0then10then3()
    {
        int[] rolls = { 0,10, 3 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T10_9thFrame_firstball()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T11_9thFrame_noSpare()
    {
        int[] rolls = { 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3,5 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T12_9thFrame_Spare()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,7 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T13_9thFrame_Strike()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 10 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T20_10thFrame_3()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T21_10thFrame_53()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 5,3 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T22_10thFrame_Spare()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 9,1 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T23_10thFrame_Spareandextra()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 9,1,6 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T24_10thFrame_Spareandextra10()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 4,6,10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T25_10thFrame_Strike()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 10 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T26_10thFrame_Strikeandextra3()
    {
        int[] rolls = { 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 3,4, 10,3 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T27_10thFrame_Strikeandextra35()
    {
        int[] rolls = { 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 10,3,5 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T28_10thFrame_Strikeandextra10()
    {
        int[] rolls = { 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 10, 10 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T29_10thFrame_Strikeandextra1010()
    {
        int[] rolls = { 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 10,10,10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

}
