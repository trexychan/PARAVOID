using System;
using System.Collections;
using System.Collections.Generic;

namespace Paravoid.DataStructures
{
    /// <summary>
    /// Priority Queue class used for A* Pathfinding Algorithm.
    /// 
    /// Uses MinHeap building.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TPriority"></typeparam>
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {

        /// <summary>
        /// Used to hold entries into the Priority Queue.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TPriority"></typeparam>
        class PriorityQueueEntry
        {
            private TElement element;
            private TPriority priority;

            /// <summary>
            /// Creates a new Priority Queue entry.
            /// </summary>
            /// <param name="element">Element in the entry.</param>
            /// <param name="priority">Priority of the respective element.</param>
            public PriorityQueueEntry(TElement element, TPriority priority)
            {
                this.element = element;
                this.priority = priority;
            }

            /// <summary>
            /// Element of the priority queue entry.
            /// </summary>
            public TElement Element
            {
                get
                {
                    return element;
                }
                set
                {
                    element = value;
                }
            }

            /// <summary>
            /// Priority of the element in the priority queue entry.
            /// </summary>
            public TPriority Priority
            {
                get
                {
                    return priority;
                }
                set
                {
                    priority = value;
                }
            }

        }


        private PriorityQueueEntry[] backingArray;
        private const int INIT_CAPACITY = 9;

        private int size;

        /// <summary>
        /// Size of the Priority Queue.
        /// </summary>
        public int Count
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Initializes an empty Priority Queue
        /// </summary>
        public PriorityQueue()
        {
            this.backingArray = new PriorityQueueEntry[INIT_CAPACITY];
            this.size = 0;
        }

        /// <summary>
        /// Reorganizes the data into a MinHeap based on TPriority values.
        /// </summary>
        private void BuildHeap()
        {
            bool didSwap = false;
            for (int i = size / 2; i > 0; i--)
            {
                int childLeftInd = 2 * i;
                int childRightInd = 2 * i + 1;
                int minChildInd;
                if (backingArray[childLeftInd] == null)
                {
                    continue;
                }
                else
                {
                    if (childRightInd >= backingArray.Length || backingArray[childRightInd] == null)
                    {
                        minChildInd = childLeftInd;
                    }
                    else
                    {
                        minChildInd = backingArray[childLeftInd].Priority
                            .CompareTo(backingArray[childRightInd].Priority) < 0
                            ? childLeftInd : childRightInd;
                    }
                    if (backingArray[minChildInd].Priority.CompareTo(backingArray[i].Priority) < 0)
                    {
                        PriorityQueueEntry temp = backingArray[i];
                        backingArray[i] = backingArray[minChildInd];
                        backingArray[minChildInd] = temp;
                        didSwap = true;
                    }
                }
            }
            if (didSwap)
            {
                BuildHeap();
            }
        }



        /// <summary>
        /// Resizes the backing array of the Priority Queue
        /// </summary>
        private void Resize()
        {
            PriorityQueueEntry[] temp = new PriorityQueueEntry[this.backingArray.Length * 2];
            for (int i = 1; i <= this.backingArray.Length; i++)
            {
                temp[i] = this.backingArray[i];
            }
            this.backingArray = temp;
        }

        /// <summary>
        /// Creates a new Priority Queue entry with the element and priority
        /// input values, adds that entry to the Priority Queue, and rebuilds the
        /// Priority Queue.
        /// </summary>
        /// <param name="element">Element being added to the Priority Queue.</param>
        /// <param name="priority">Priority of the element being added.</param>
        public void Enqueue(TElement element, TPriority priority)
        {
            PriorityQueueEntry entry = new PriorityQueueEntry(element, priority);
            if (size + 1 == backingArray.Length)
            {
                Resize();
            }
            this.backingArray[++size] = entry;
            BuildHeap();
        }

        /// <summary>
        /// Removes the element in the front of the Priority Queue and rebuilds the
        /// Priority Queue.
        /// </summary>
        /// <returns>The element with the lowest priority value.</returns>
        public TElement Dequeue()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("Cannot dequeue from an empty Priority Queue!");
            }
            TElement removed = backingArray[1].Element;
            backingArray[1] = backingArray[size];
            backingArray[size--] = null;
            BuildHeap();
            return removed;
        }

        /// <summary>
        /// Gets the element in the Priority Queue with the lowest priority measurement.
        /// </summary>
        /// <returns>The element with the lowest priority in the Priority Queue</returns>
        public TElement GetMinPriorityElement()
        {
            return this.backingArray[1].Element;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        /// <summary>
        /// Checks if the Priority Queue contains the given element.
        /// </summary>
        /// <param name="element">The element to search for in the Priority Queue</param>
        /// <returns>true if the element exists, false if otherwise</returns>
        public bool Contains(TElement element)
        {
            for (int i = 1; i <= size; i++)
            {
                if (backingArray[i].Element.Equals(element))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Used to remove a particular value from the Priority Queue
        /// </summary>
        /// <param name="element"></param>
        /// <returns>The element removed from the Priority Queue</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the element is not found in the Priority Queue</exception>
        public TElement Remove(TElement element)
        {
            int i = 0;
            for (int j = 1; j < size; j++)
            {
                if (backingArray[j].Element.Equals(element))
                {
                    i = j;
                    break;
                }
            }
            if (i == 0)
            {
                throw new KeyNotFoundException("Element does not exist, cannot be removed from the Priority Queue!");
            }
            else
            {
                TElement temp = backingArray[i].Element;
                backingArray[i] = null;
                for (int j = i; j < size; j++)
                {
                    backingArray[j] = backingArray[j + 1];
                }
                size--;
                BuildHeap();
                return temp;
            }

        }
    }

}
