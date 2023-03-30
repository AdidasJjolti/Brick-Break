using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface ISubject
    {
        void RegisterObserver(int key, IObserver observer);
        void RemoveObserver(int key, IObserver observer);
        void NotifyObservers(int key, bool isTrigger);
    }

    public interface IObserver
    {
        void SwitchOnTrigger(bool isTrigger);
    }
}