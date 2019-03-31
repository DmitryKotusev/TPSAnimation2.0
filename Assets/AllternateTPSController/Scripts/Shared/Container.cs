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

        public ContainerItem()
        {
            id = System.Guid.NewGuid();
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

    private void Awake()
    {
        items = new List<ContainerItem>();
    }

    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            maximum = maximum,
            name = name,
        }
        );
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
