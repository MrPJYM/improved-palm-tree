using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
        public static SkillPool Instance;

        [SerializeField]
        private GameObject poolingObjectPrefab;

        Queue<SkillObj> poolingObjectQueue = new Queue<SkillObj>();

        private void Awake()
        {
            Instance = this;

            Initialize(5);
        }

        private void Initialize(int initCount)
        {
            for (int i = 0; i < initCount; i++)
            {
                poolingObjectQueue.Enqueue(CreateNewObject());
            }
        }

        private SkillObj CreateNewObject()
        {
            var newObj = Instantiate(poolingObjectPrefab).GetComponent<SkillObj>();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }

        public static SkillObj GetObject()
        {
            if (Instance.poolingObjectQueue.Count > 0)
            {
                var obj = Instance.poolingObjectQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewObject();
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }

        public static void ReturnObject(SkillObj obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(Instance.transform);
            Instance.poolingObjectQueue.Enqueue(obj);
        }
}
