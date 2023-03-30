using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;

public static class BrickData
{
    static BrickObserver ob;

    public static BrickObserver GetBrickData()
    {
        if (ob == null)
            ob = new BrickObserver();
        return ob;
    }


    public class BrickObserver : ISubject
    {
        // ItemBrick Ŭ������ ������ 2�� �̻� �ִ� ���� �����Ͽ� ��� ItemBrick Ŭ�������� SwitchOnTrigger �Լ��� ������ �������� ����Ʈ�� ���
        Dictionary<int, List<IObserver>> observerDictionary = new Dictionary<int, List<IObserver>>();
        List<IObserver> observers = new List<IObserver>();


        // �� ItemBrick�� InstanceID�� Ű������ �ϴ� ��ųʸ� ����
        public void InitDictionary(int instanceID)
        {
            observerDictionary[instanceID] = new List<IObserver>();
        }

        public void AddDictionary()
        {
            var bricks = GameObject.FindObjectsOfType<Brick>();

            foreach (var item in observerDictionary)
            {
                for(int i = 0; i <bricks.Length; i++)
                {
                    RegisterObserver(item.Key, bricks[i]);
                }
            }
        }

        public void RegisterObserver(int key, IObserver observer)
        {
            observers.Add(observer);

            // key���� ���� �ϴ��� üũ
            if(observerDictionary.ContainsKey(key) == false)
            {
                return;
            }
            List<IObserver> obs = observerDictionary[key];
            obs.Add(observer);
        }

        public void RemoveObserver(int key, IObserver observer)
        {
            observers.Remove(observer);

            if (observerDictionary.ContainsKey(key) == false)
            {
                return;
            }
            List<IObserver> obs = observerDictionary[key];
            obs.Remove(observer);
        }

        public void NotifyObservers(int key, bool isTrigger)
        {
            // Paddle.CheckMissileCount()���� key���� -1�� �޾ƿͼ� ���� collider�� isTrigger�� false�� ����
            if (key == -1)
            {
                foreach (IObserver ob in observers)
                {
                    ob.SwitchOnTrigger(isTrigger);
                }
                return;
            }

            if (observerDictionary.ContainsKey(key) == false)
            {
                return;
            }
            List<IObserver> obs = observerDictionary[key];
            foreach (IObserver ob in obs)
            {
                ob.SwitchOnTrigger(isTrigger);
            }
        }
    }
}
