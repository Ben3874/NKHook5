﻿using Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NKHook5
{
    public class Game
    {
        [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);

        static Game instance = null;
        static Process gameProc = null;
        static Mem memlib = Program.memlib;

        public static Game getBTD5()
        {
            return instance;
        }
        public static Process getProcess()
        {
            return gameProc;
        }
        internal Game(Process gameProc)
        {
            Game.gameProc = gameProc;
            instance = this;
        }

        /*
         * Getters here
         */
        public List<Tower> getTowers()
        {
            List<Tower> returntowers = new List<Tower>();
            foreach(int towerId in MemScanner.allTowers)
            {
                Tower tower = new Tower(towerId);
                returntowers.Add(tower);
            }
            return returntowers;
        }

        int prevSelected = 0;
        public Tower getSelectedTower()
        {
            try
            {
                if (MemScanner.selectedTowers.Count > 0)
                {
                    foreach(int towerId in MemScanner.selectedTowers)
                    {
                        if (towerId == prevSelected)
                        {
                            return new Tower(towerId);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return new Tower(MemScanner.selectedTowers[MemScanner.selectedTowers.Count - 1]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                /*
                Logger.Log("Exception caught. Ths was expected in 'Game.getSelectedTowers'");
                Logger.Log(ex.StackTrace);
                */
                return null;
            }
        }
        public double getMoney()
        {
            return memlib.readDouble("BTD5-Win.exe+008844B0,0xC4,0x90");
        }
        public int getMonkeyMoney()
        {
            return memlib.readInt("BTD5-Win.exe+008844B0,0xD4,0x18,0x0,0x58,0x118");
        }

        /*
         * Setters here
         */
        public void setGameTitle(string title)
        {
            SetWindowText(gameProc.MainWindowHandle, title);
        }
        public void setMoney(int amount)
        {
            memlib.writeMemory("BTD5-Win.exe+008844B0,0xC4,0x90", "double", amount.ToString());
        }
        public void setMonkeyMoney(int amount)
        {
            memlib.writeMemory("BTD5-Win.exe+008844B0,0xD4,0x18,0x0,0x58,0x118", "int", amount.ToString());
        }
    }
}
