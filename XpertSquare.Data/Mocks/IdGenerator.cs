using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Data.Mocks
{
    public static class IdGenerator
    {
        static Hashtable idTable = new Hashtable();
        public static int GetNextID(String entityType)
        {
            int nextID = 0;
            if (idTable.Contains(entityType))
            {                
                nextID = Convert.ToInt32(idTable[entityType]) + 1;
                idTable[entityType] = nextID;
            }
            else
            {
                idTable.Add(entityType, nextID);                
            }
            return nextID;
        }
    }
}
