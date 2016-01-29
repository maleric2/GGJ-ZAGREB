using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ListView
{
    /// <summary>
    /// Redak u listi
    /// TODO: omogućit da je listRow header
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListRow<T>
    {
        #region Properties
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set {
                if (GameObject != null && GameObject.GetComponentInChildren<UnityEngine.UI.Toggle>() != null)
                {
                    GameObject.GetComponentInChildren<UnityEngine.UI.Toggle>().isOn = value;
                }
                selected = value; 
            }
        }
        private T obj;

        public T Obj
        {
            get { return obj; }
            set { obj = value; }
        }
        

        private UnityEngine.GameObject gameObject;
        /// <summary>
        /// GameObject Reference
        /// </summary>
        public UnityEngine.GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }
        
        private List<ListItem> items;

        #endregion

        /// <summary>
        /// Important use correct ID
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        public void Initialize(T obj, int id)
        {
            this.obj = obj;
            this.id = id;
            items = new List<ListItem>();
        }

        public T GetObject()
        {
            return this.obj;
        }

        public void AddItem(ListItem item)
        {
            items.Add(item);
        }
        public List<ListItem> GetItems()
        {
            return items;
        }

        internal void OnToggleClick(bool value)
        {
            this.selected = value;
        }
    }
}
