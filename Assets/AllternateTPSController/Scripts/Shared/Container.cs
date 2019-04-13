using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

        internal void AddAmount(int amount)
        {
            amountTaken -= amount;
            if (amountTaken < 0)
            {
                amountTaken = 0;
            }
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

    public void Put(string name, int amount)
    {
        ContainerItem item = items.Where(x => x.name == name).FirstOrDefault();
        if (item == null)
        {
            return;
        }
        item.AddAmount(amount);
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
        {
            return -1;
        }
        return containerItem.Get(amount);
    }

    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
        {
            return -1;
        }
        return containerItem.Remaining;
    }

    public ContainerItem GetContainerItem(System.Guid id)
    {
        return items.Where(x => x.id == id).FirstOrDefault();
    }
}
