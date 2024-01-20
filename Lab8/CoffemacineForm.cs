using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Lab8;

namespace Lab8
{
    public partial class CoffemacineForm : Form
    {
        enum MachineState
        {
            ModeSelection,

            AmericanoMaking,
            CappuchinoMaking
        }

        enum MachineEvent
        {
            AmericanoChosen,
            CappuchinoChosen,

            MakingComplete
        }

        FSM<MachineState, MachineEvent> FSM;
        Coffemachine coffemachine = new Coffemachine();

        public CoffemacineForm()
        {
            InitializeComponent();

            FSM = new FSM<MachineState, MachineEvent>(MachineState.ModeSelection);


            FSM.RegisterTransitions(new Transition<MachineState, MachineEvent>[]
            {
                new Transition<MachineState, MachineEvent> {FromState = MachineState.ModeSelection, ToState = MachineState.AmericanoMaking, Event = MachineEvent.AmericanoChosen},
                new Transition<MachineState, MachineEvent> {FromState = MachineState.ModeSelection, ToState = MachineState.CappuchinoMaking, Event = MachineEvent.CappuchinoChosen},

                new Transition<MachineState, MachineEvent> {FromState = MachineState.AmericanoMaking, ToState = MachineState.ModeSelection, Event = MachineEvent.MakingComplete},
                new Transition<MachineState, MachineEvent> {FromState = MachineState.CappuchinoMaking, ToState = MachineState.ModeSelection, Event = MachineEvent.MakingComplete}
            });

            FSM[MachineState.ModeSelection].OnExit = () => coffemachine.StartIngreedientsCheck(button1, button2, label1);

            FSM[MachineState.AmericanoMaking].OnEnter = () =>
            {
                coffemachine.AmericanoMaker(label1, this);
                FSM.OnEvent(MachineEvent.MakingComplete);
            };
            FSM[MachineState.AmericanoMaking].OnExit = () => coffemachine.EndMaker(button1, button2, label1);

            FSM[MachineState.CappuchinoMaking].OnEnter = () => 
            {
                coffemachine.CappuchinoMaker(label1, this);
                FSM.OnEvent(MachineEvent.MakingComplete);
            };
            FSM[MachineState.CappuchinoMaking].OnExit = () => coffemachine.EndMaker(button1, button2, label1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FSM.OnEvent(MachineEvent.AmericanoChosen);
        }
        private void button2_Click(object sender, EventArgs e)
        {
           FSM.OnEvent(MachineEvent.CappuchinoChosen);
        }
    }
}
