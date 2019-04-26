// ReSharper disable RedundantUsingDirective
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    sealed class Program : MyGridProgram
    {
        private DateTime _initiationDateTime = DateTime.Now;

        public Program()
        {
            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script. 
            //     
            // The constructor is optional and can be removed if not
            // needed.
            // 
            // It's recommended to set RuntimeInfo.UpdateFrequency 
            // here, which will allow your script to run itself without a 
            // timer block.

            Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }

        public void Save()
        {
            Storage = _arg;
        }

        private string _arg = "";

        private int _cnt;

        public void Main(string argument, UpdateType updateSource)
        {
            if (!string.IsNullOrEmpty(argument))
            {
                _arg = argument;
                Save();
            }
            else
            {
                _arg = Storage;
            }

            // The main entry point of the script, invoked every time
            // one of the programmable block's Run actions are invoked,
            // or the script updates itself. The updateSource argument
            // describes where the update came from. Be aware that the
            // updateSource is a  bitfield  and might contain more than 
            // one update type.
            // 
            // The method itself is required, but the arguments above
            // can be removed if not needed.

            string o = "";

            List<IMyAirtightHangarDoor> airtightHangarDoors = new List<IMyAirtightHangarDoor>();
            List<IMyProgrammableBlock> programmableBlocks = new List<IMyProgrammableBlock>();

            GridTerminalSystem.GetBlocksOfType(airtightHangarDoors);
            GridTerminalSystem.GetBlocksOfType(programmableBlocks);

            foreach (IMyProgrammableBlock block in programmableBlocks)
            {
                if (block.CustomName != _arg)
                    block.ApplyAction("OnOff_Off");
            }

            foreach (IMyAirtightHangarDoor b in airtightHangarDoors)
            {
                b.OpenDoor();
            }

            if ((DateTime.Now - _initiationDateTime).TotalSeconds > 15)
            {

            }

            _cnt++;

            Echo(o);
        }
    }
}