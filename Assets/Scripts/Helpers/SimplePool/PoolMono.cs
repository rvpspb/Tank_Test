using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T prefab { get; }
        public bool autoExpand { get; set; } // автоматически создавать новые объекты для пула, если не хватает елементов
        public Transform container { get; }

        private List<T> _pool;

        public PoolMono(T prefab, int count)
        {
            this.prefab = prefab;
            this.container = null;

            CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container)
        {
            this.prefab = prefab;
            this.container = container;
            CreatePool(count);

        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        /// <summary>
        /// Создает новый объект
        /// </summary>
        /// <param name="isActiveByDefault">активен или нет</param>
        /// <returns>созданный объект</returns>
        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(this.prefab, this.container);
            createdObject.gameObject.SetActive(isActiveByDefault);

            _pool.Add(createdObject);

            return createdObject;
        }

        /// <summary>
        /// Проверяет, есть ли указанный элемент в пуле, если есть, то помещает его в "element"
        /// </summary>
        /// <param name="element"></param>
        public bool HasFreeElement(out T element)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].gameObject.activeInHierarchy)
                {
                    element = _pool[i];
                    _pool[i].gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
            {
                return element;
            }

            if (autoExpand)
            {
                return CreateObject(isActiveByDefault: true);
            }

            throw new System.Exception(message: $"There is no free elements in pool of type {typeof(T)}");
        }
    }
}
