using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Container : MonoBehaviour
{
    [System.Serializable]
    public class ContainerItem
    {
        public System.Guid id;
        public string name;
        public int maximum;

        public int amountTaken;

        public ContainerItem(string name, int maximum)
        {
            id = System.Guid.NewGuid();
            this.maximum = maximum;
            this.name = name;
        }

        public int Remaining
        {
            get
            {
                return maximum - amountTaken;
            }
        }

        public int Get(int amount)
        {
            if (amountTaken + amount > maximum)
            {
                int toGive = maximum - amountTaken;
                amountTaken = maximum;
                return toGive;
            }
            amountTaken += amount;
            return amount;
        }
    }

    public List<ContainerItem> items;

    public System.Guid Add(string name, int maximum)
    {
        if(items == null)
        {
            items = new List<ContainerItem>();
        }
        items.Add(new ContainerItem(name, maximum));
        return items.Last().id;
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = items.Where(x => x.id == id).FirstOrDefault();
        if(containerItem == null)
        {
            return -1;
        }
        return containerItem.Get(amount);
    }
}
