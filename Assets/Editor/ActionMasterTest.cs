using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;


    [SetUp]
    public void Setup ()  // runs at the beginning of *each* test
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00_PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10) );
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {        
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03_SecondBowlReturnsEndTurn()
    {
        actionMaster.Bowl(3);
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T04_SpareReturnsEndTurn()
    {
        actionMaster.Bowl(3);
        Assert.AreEqual(endTurn, actionMaster.Bowl(7));
    }

    [Test]
    public void T05_Bowl0()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T06_Bowl0then10()
    {
        actionMaster.Bowl(0);
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T07_Bowl0then10then3()
    {
        actionMaster.Bowl(0);
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void T10_9thFrame_firstball()
    {
        for (int i = 1; i < 9; i++)  // 8 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void T11_9thFrame_noSpare()
    {
        for (int i = 1; i < 9; i++)  // 8 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(3);
        Assert.AreEqual(endTurn, actionMaster.Bowl(5) );
    }

    [Test]
    public void T12_9thFrame_Spare()
    {
        for (int i = 1; i < 9; i++)  // 8 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(3);
        Assert.AreEqual(endTurn, actionMaster.Bowl(7) );
    }

    [Test]
    public void T13_9thFrame_Strike()
    {
        for (int i = 1; i < 9; i++)  // 8 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        Assert.AreEqual(endTurn, actionMaster.Bowl(10) );
    }

    [Test]
    public void T20_10thFrame_3()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(3) );
    }

    [Test]
    public void T21_10thFrame_53()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(5);
        Assert.AreEqual(endGame, actionMaster.Bowl(3) );
    }

    [Test]
    public void T22_10thFrame_Spare()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(9);
        Assert.AreEqual(reset, actionMaster.Bowl(1) );
    }

    [Test]
    public void T23_10thFrame_Spareandextra()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(9);
        actionMaster.Bowl(1);
        Assert.AreEqual(endGame, actionMaster.Bowl(6) );
    }

    [Test]
    public void T24_10thFrame_Spareandextra10()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(4);
        actionMaster.Bowl(6);
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
    
    [Test]
    public void T25_10thFrame_Strike()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T26_10thFrame_Strikeandextra3()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(3) );
    }

    [Test]
    public void T27_10thFrame_Strikeandextra35()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(10);
        actionMaster.Bowl(3);
        Assert.AreEqual(endGame, actionMaster.Bowl(5));
    }

    [Test]
    public void T28_10thFrame_Strikeandextra10()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(reset, actionMaster.Bowl(10) );
    }

    [Test]
    public void T29_10thFrame_Strikeandextra1010()
    {
        for (int i = 1; i < 10; i++)  // 9 frames, no strikes or spares
        {
            actionMaster.Bowl(3);
            actionMaster.Bowl(4);
        }
        actionMaster.Bowl(10);
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

}
