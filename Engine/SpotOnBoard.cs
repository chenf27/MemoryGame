using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public struct SpotOnBoard
    {
        int m_Row;
        int m_Col;

        public int Row
        { 
            get { return m_Row; } 
            set { m_Row = value; }
        }
        public int Col
        { 
            get { return m_Col; } 
            set { m_Col = value; }
        }
        public SpotOnBoard(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }
    }
}
