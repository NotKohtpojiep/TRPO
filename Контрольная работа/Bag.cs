using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlWork
{
    public class Bag
    {
        public List<Thing> Things { get; set; }
        public int CountOfThigs
        {
            get => Things != null ? Things.Count : 0;
        }

        public double AvgThingWeight
        {
            get => Things.Average(x => x.Weight);
        }
        public bool IsGoodForTaking
        {
            get
            {
                bool isCorrect = Things != null;
                if (Things != null)
                {
                    foreach (var thing in Things)
                    {
                        if (Math.Abs(Things.Average(x => x.Weight) - thing.Weight) > 0.3)
                        {
                            isCorrect = false;
                        }
                    }
                }
                return isCorrect;
            }
        }
    }

}
