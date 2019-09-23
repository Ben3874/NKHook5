﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook5.Events
{
    public class HealthIncreaseEvent : NkEvent
    {
        public static event EventHandler<EventArgs> Event;
        public override void work(object sender, DoWorkEventArgs e)
        {
            base.work(sender, e);

            int health = 150;
            //Event work
            while (true)
            {
                int newHealth = memlib.readInt("BTD5-Win.exe+00884274,0x5C,0x8C,0x18,0xC8,0x88");
                if (newHealth > health)
                {
                    try
                    {
                        Event.Invoke(this, new EventArgs());
                    }
                    catch (NullReferenceException) { }
                }
                health = newHealth;
            }
        }
    }
}
