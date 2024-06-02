using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameEngine
{
    public struct BoardSlot
    {
        private readonly char r_CardInSlot;
        private bool m_CardFlippedByPlayer;

        public BoardSlot(char i_CardInSlot)
        {
            r_CardInSlot = i_CardInSlot;
            m_CardFlippedByPlayer = false;
        }

        public char CardInSlot
        {
            get { return r_CardInSlot; }
        }

        public bool CardFlippedByPlayer
        {
            get { return m_CardFlippedByPlayer; }
            set { m_CardFlippedByPlayer = value; }
        }
    }
}
