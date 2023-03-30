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
        // ItemBrick 클래스가 씬에서 2개 이상 있는 것을 감안하여 모든 ItemBrick 클래스마다 SwitchOnTrigger 함수를 실행할 옵저버를 리스트로 등록
        Dictionary<int, List<IObserver>> observerDictionary = new Dictionary<int, List<IObserver>>();
        List<IObserver> observers = new List<IObserver>();


        // 각 ItemBrick의 InstanceID를 키값으로 하는 딕셔너리 생성
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

            // key값이 존재 하는지 체크
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
            // Paddle.CheckMissileCount()에서 key값을 -1로 받아와서 벽돌 collider의 isTrigger를 false로 변경
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
