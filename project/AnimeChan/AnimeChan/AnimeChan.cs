using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeChanNS 
{
    class SetEnum<T> where T : Enum
    {
        public SetEnum(T value)
        {
            _value = value;
        }

        public void Add(T value)
        {
            _value |=
        }

        T _value;
        public T Value { get { return _value; } }
    }

    class AnimeChan
    {
        public AnimeChan()
        {

        }

        public AnimeChan(Clothes clothes)
        {
            DollClothes = clothes;
        }

        public AnimeChan(params Clothes[] clothes)
        {
            foreach (Clothes item in clothes)
            {
                DollClothes |= item;
            }
        }

        public void WhatIsDressed()
        {
            if (DollClothes == Clothes.None)
            {
                Console.WriteLine("I'm naked");
            } 
            else
            {
                Console.Write("I'm wearing: ");
                int dollClothes = (int)DollClothes;
                for (int i = 8; i >= 0; i--)
                {
                    int shift = 1 << i;
                    if (dollClothes >= shift)
                    {
                        Console.Write((Clothes)shift + ", ");
                        dollClothes ^= shift;
                    }
                }
            }
        }
        Clothes DollClothes { get; set; } = Clothes.None;
    }
}
